﻿using System;
using System.Collections.Generic;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class ACL200 : Rs232Base
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);
                string tempString = StringData;
                while (!Char.IsLetterOrDigit(tempString[0]))
                {
                    tempString = tempString.Remove(0, 1);
                }
                File.AppendAllText(@"C:\ACL-KETQUA.txt", StringData);
                //string pYear = null;

                //string[] strResutl = DeviceHelper.DeleteAllBlankLine(strResult, DeviceHelper.CRLF);

                var strResutl = new string[] {};

                int id = 0;
                foreach (string s in tempString.Split(DeviceHelper.CRLF.ToCharArray()))
                {
                    if (s.Trim() != "")
                    {
                        id++;
                        Array.Resize(ref strResutl, id);
                        strResutl[id - 1] = s;
                    }
                }
                //Lay ve ngay thang nam lam xn
                string pMonth = tempString.Substring(5).Substring(4, 5).Replace(".", "").Trim();
                //lấy tháng
                pMonth = DeviceHelper.GetMonth(pMonth);
                //Lấy ngày
                string pDay = tempString.Substring(5).Substring(0, 3).Replace(".", "").Trim();
                //lấy Năm
                string pYear = tempString.Substring(5).Substring(9, 4).Trim();
                if (pDay.Length == 1)
                {
                    pDay = "0" + pDay;
                }
                string tempTestDate = string.Format("{0}/{1}/20{2}", pDay, pMonth, pYear);

                string[] arrPatients = SeparatorData(strResutl);
                //duyệt từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    TestResult.TestDate = tempTestDate;
                    string[] allRecord = patient.Split(DeviceHelper.LF);
                    TestResult.Barcode = allRecord[0];
                    string resultString = allRecord[1].Substring(0, 8);
                    if ((resultString.IndexOf(' ') > 0) || (resultString.IndexOf('*') > 0))
                    {
                        AddResult(new ResultItem("PT", allRecord[1].Substring(0, 5).Trim()));
                        AddResult(new ResultItem("PT(%)", allRecord[1].Substring(5, 4).Trim()));
                        AddResult(new ResultItem("PT-INR", allRecord[1].Substring(9, 5).Trim()));
                        AddResult(new ResultItem("FIB", allRecord[1].Substring(14, 4).Trim()));
                        Log.Debug("Barcode:{0} PT:{1} PT (%):{2} PT-INR:{3} FIB:{4}", TestResult.Barcode,
                            allRecord[1].Substring(0, 5).Trim(), allRecord[1].Substring(5, 4).Trim(),
                            allRecord[1].Substring(9, 5).Trim(), allRecord[1].Substring(14, 4).Trim());
                    }
                    else
                    {
                        AddResult(new ResultItem("APTT", allRecord[1].Substring(0, 4).Trim()));
                        AddResult(new ResultItem("APTT-INR", allRecord[1].Substring(4, 4).Trim()));
                        Log.Debug("Barcode:{0} APTT:{1} APTT-INR:{2}", TestResult.Barcode,
                            allRecord[1].Substring(0, 4).Trim(), allRecord[1].Substring(4, 4).Trim());
                    }
                    ImportResults();
                    Log.Debug("Finish Import Result");
                }
                Log.Trace("Finish Process Data {0}", DeviceHelper.CRLF);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu", ex));
            }
            finally
            {
                ClearData();
            }
        }

        private string[] SeparatorData(string[] stringData)
        {
            var arrResult = new string[] {};
            try
            {
                var arrBarcode = new List<string>();
                int id = 0;
                for (int index = stringData.Length - 1; index >= 0; index--)
                {
                    string s = stringData[index];
                    if ((s.Trim() != "") && (s.StartsWith("        ")))
                    {
                        arrBarcode.Insert(0, DeviceHelper.DeleteAllBlankLine(s, ' ')[0]);
                    }
                    else
                    {
                        break;
                    }
                }
                id = 0;
                for (int i = 3; i < stringData.Length - arrBarcode.Count; i++)
                {
                    string[] tempStringArr = DeviceHelper.DeleteAllBlankLine(stringData[i], ' ');
                    int barcodeIndex = Convert.ToInt32(tempStringArr[tempStringArr.Length - 1]);
                    if (Char.IsLetter(tempStringArr[0][0]))
                    {
                        Log.Error("Barcode {0} bi loi ket qua", arrBarcode[barcodeIndex - 1]);
                        continue;
                    }
                    id++;
                    Array.Resize(ref arrResult, id);
                    arrResult[id - 1] = arrBarcode[barcodeIndex - 1] + DeviceHelper.LF + stringData[i];
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình tách dữ liệu mẫu", ex));
            }
            return arrResult;
        }
    }
}