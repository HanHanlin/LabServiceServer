﻿using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Bs800 : TcpIpAstmDevice
    {
        public override void ProcessData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace(DeviceHelper.CRLF + StringData);

                //Lấy về dữ liệu của các bệnh nhân
                string[] arrPatients = SeparatorData(StringData);

                Log.Trace("Result has {0} Patients", arrPatients.Length);
                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("H"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                string tempDate = temp[13];
                                TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                    tempDate.Substring(4, 2),
                                    tempDate.Substring(0, 4));
                            }
                            catch (Exception)
                            {
                                TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }
                        else if (record.StartsWith("O"))
                            try
                            {
                                temp = record.Split('|');
                                TestResult.Barcode = temp[3].Trim();
                                Log.Debug("Barcode:{0}", TestResult.Barcode);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while parse data: {0}{1}Eror String:{2}", record, DeviceHelper.CRLF,
                                    ex.ToString());
                            }
                        else if (record.StartsWith("R"))
                            try
                            {
                                temp = record.Split('|');

                                string testName = temp[2].Split('^')[0].Trim();
                                string testValue = temp[3].Split('^')[0].Trim();
                                string testUnit = temp[4].Trim();
                                AddResult(new ResultItem(testName, testValue, testUnit));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while parse data: {0}{1}Eror String:{2}", record, DeviceHelper.CRLF,
                                    ex.ToString());
                            }
                    }

                    Log.Debug(string.Format("Import Result For barocde"));
                    Log.Debug(ImportResults()
                        ? string.Format("Import Result Success")
                        : string.Format("Error while import result"));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        private string[] SeparatorData(string stringData)
        {
            try
            {
                //Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                stringData = stringData.Substring(stringData.IndexOf("H|", StringComparison.Ordinal));

                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;
                foreach (string line in arrStringData)
                {
                    if (string.IsNullOrEmpty(line)) continue;
                    if (line.StartsWith("H"))
                    {
                        id++;
                        Array.Resize(ref allResult, id);
                    }
                    allResult[id - 1] = allResult[id - 1] + line + DeviceHelper.CR;
                }
                return allResult;
            }
            catch (Exception ex)
            {
                Log.Error("Error while parsing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                throw;
            }
        }
    }
}