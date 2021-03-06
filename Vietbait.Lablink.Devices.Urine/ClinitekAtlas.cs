﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class ClinitekAtlas : Rs232AstmDevice
        //internal class XS1000I : Rs232Base
    {
        /// <summary>
        ///     Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);

                //Lấy về dữ liệu của các bệnh nhân
                IEnumerable<string> arrPatients = SeparatorData(StringData);

                Log.Debug("Find {0} Result:", arrPatients.Count());

                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                        {
                            temp = record.Split('|');
                            //Lấy barcode xét nghiệm
                            try
                            {
                                string strTempbarcode = temp[2].Trim().Split('^')[0].Trim();
                                TestResult.Barcode = strTempbarcode == "" ? "0000" : strTempbarcode;
                            }
                            catch (Exception)
                            {
                                TestResult.Barcode = "0000";
                            }
                            string tempDate = temp[22];
                            TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                tempDate.Substring(4, 2),
                                tempDate.Substring(0, 4));
                        }
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');

                                if (Convert.ToInt32(temp[1]) < 11)
                                {
                                    string strTestName = temp[2].Split('^')[3].Trim();
                                    string strTestValue = temp[3].Trim();
                                    string strTestUnit = temp[4].Trim();

                                    AddResult(new ResultItem(strTestName, strTestValue, strTestUnit));
                                    Log.Trace("Add Result success:TestName:{0} - TestValue:{1} - TestUnit:{2}",
                                        strTestName, strTestValue, strTestUnit);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while add new result\n\rException:{0}", ex);
                            }
                        }
                    }
                    Log.Debug("Begin Import result for barcode:{0}", TestResult.Barcode);
                    Log.Debug(ImportResults() ? "Import Result Success" : "False while import Result");
                }
                Log.Trace("Finish Process Data {0}", DeviceHelper.CRLF);
            }
            catch (Exception ex)
            {
                // throw ex;
                Log.Error("Eror while process Data", ex);
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string stringData)
        {
            try
            {
                //Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                stringData =
                    stringData.Substring(stringData.IndexOf(DeviceHelper.CR + "P|", StringComparison.Ordinal) + 1);

                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrStringData)
                {
                    if (line.StartsWith("P"))
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
                throw ex;
            }
        }
    }
}