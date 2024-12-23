
/*==============================================================================================================
INI Class 
---------
Author:
				Adam Woods - http://www.aejw.com/
Modified:
				3rd August 2005
Ownership:
				Copyright 2005 Adam Woods
Build:
				0019 - see http://www.codeproject.com/csharp/aejw_ini_class.asp for more information and histroy
Source:
				http://www.aejw.com/
EULA:
				You disturbe and use this code / class in any envoriment you see fit.
				The header (this information) can not be modified or removed.
				LIMIT OF LIABILITY: IN NO EVENT WILL Adam Woods BE LIABLE TO YOU FOR ANY LOSS OF USE, 
				INTERRUPTION OF BUSINESS, OR ANY DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL 
				DAMAGES OF ANY KIND (INCLUDING LOST PROFITS) REGARDLESS OF THE FORM OF ACTION WHETHER IN 
				CONTRACT, TORT (INCLUDING NEGLIGENCE), STRICT PRODUCT LIABILITY OR OTHERWISE, EVEN 
				IF Adam Woods HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.EULA:
				
������������ ����������:
                �� ������ ������������ ���� ��� / ����� � ����� �����, ������� ������� ������.
                ��������� (��� ����������) �� ����� ���� ������� ��� ������.
                ������ ���������������: �� � ���� ������ Adam Woods �� ����� ����� ��������������� �����
                ���� �� ����� ������ �������������,
                ���������� ������� ��� ����� ������, ���������, �����������, ��������� ��� ����������
                ������ ������ ���� (������� ��������� ������) ���������� �� ����� ����, ���� �� �
                ���������, ������� (������� ����������), ������� ��������������� �� ������� ��� ���� 
                �������, ���� ���� ���� ���� ��� ������������ � ����������� ������ ������.
 
==============================================================================================================*/
using System;
using System.Runtime.InteropServices;

namespace drz.Infrastructure.Service
{
    /// <summary>
    /// ������ � ������ � ����� ���
    /// </summary>
    public class ClsIni
    {
        [DllImport("kernel32", SetLastError = true)] private static extern int WritePrivateProfileString(string pSection, string pKey, string pValue, string pFile);
        [DllImport("kernel32", SetLastError = true)] private static extern int WritePrivateProfileStruct(string pSection, string pKey, string pValue, int pValueLen, string pFile);
        [DllImport("kernel32", SetLastError = true)] private static extern int GetPrivateProfileString(string pSection, string pKey, string pDefault, byte[] prReturn, int pBufferLen, string pFile);
        [DllImport("kernel32", SetLastError = true)] private static extern int GetPrivateProfileStruct(string pSection, string pKey, byte[] prReturn, int pBufferLen, string pFile);

        private string ls_IniFilename;
        private int li_BufferLen = 2048;//�������� �����
        //private int li_BufferLen = 512;// by razygraevaa on 16.04.2022 at 11:50
        //private int li_BufferLen = 256;// by razygraevaa on 07.02.2022 at 12:43

        /// <summary> ClsIni Constructor </summary>
        public ClsIni(string pIniFilename)
        {
            ls_IniFilename = pIniFilename;
        }

        /// <summary> INI filename (���� ���� �� ������, ������� ����� ������ ���� � �������� Windows)</summary>
        public string IniFile
        {
            get => (ls_IniFilename);
            set => ls_IniFilename = value;
        }

        /// <summary>������������ ����� �������� ��� ���������� ������ (����.: 32767) </summary>
        public int BufferLen
        {
            get => li_BufferLen;
            set
            {
                if (value > 32767) { li_BufferLen = 32767; }
                else if (value < 1) { li_BufferLen = 1; }
                else { li_BufferLen = value; }
            }
        }

        /// <summary>������ �������� �� INI File, default = pDefault</summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="pDefault">�������� �� ���������</param>
        /// <returns>�������� string</returns>
        public string ReadValue(string pSection, string pKey, string pDefault)
        {
            return (z_GetString(pSection, pKey, pDefault));
        }

        //  razygraevaa on 24.10.2022 at 13:19
        /// <summary>������ �������� �� INI File, default = pDefault</summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="ipDefault">�������� �� ��������� int</param>
        /// <returns>�������� int</returns>
        public int ReadValue(string pSection, string pKey, int ipDefault)
        {
            string pDefault = ipDefault.ToString();

            string sVal = z_GetString(pSection, pKey, pDefault);

            if (!int.TryParse(sVal, out int iVal))
            {
                iVal = ipDefault; //���� �����
            }

            return iVal;
        }

        //  razygraevaa on 24.10.2022 at 13:19
        /// <summary>������ �������� �� INI File, default = pDefault</summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="upDefault">�������� �� ��������� ulong</param>
        /// <returns>�������� ulong</returns>
        public ulong ReadValue(string pSection, string pKey, ulong upDefault)
        {
            string pDefault = upDefault.ToString();

            string sVal = z_GetString(pSection, pKey, pDefault);

            if (!ulong.TryParse(sVal, out ulong iVal))
            {
                iVal = upDefault; //���� �����
            }

            return iVal;
        }

        // ���������� ������ � ���
        //  razygraevaa on 21.11.2023 at 13:19
        /// <summary>������ �������� �� INI File, default = pDefault</summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="upDefault">�������� �� ��������� ulong</param>
        /// <returns>�������� ulong</returns>
        public bool ReadValue(string pSection, string pKey, bool upDefault)
        {
            string pDefault = upDefault.ToString();

            string sVal = z_GetString(pSection, pKey, pDefault);

            if (!bool.TryParse(sVal, out bool iVal))
            {
                iVal = upDefault; //���� �����
            }
            return iVal;
        }


        /// <summary>������ �������� �� INI File, default = "" </summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <returns>��������</returns>

        public string ReadValue(string pSection, string pKey)
        {
            return (z_GetString(pSection, pKey, ""));
        }

        /// <summary>������ �������� � INI File</summary> 
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="pValue">��������</param>
        public void WriteValue(string pSection, string pKey, string pValue)
        {
            WritePrivateProfileString(pSection, pKey, pValue, this.ls_IniFilename);
        }

        /// <summary>������� �������� �� INI File</summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        public void RemoveValue(string pSection, string pKey)
        {
            WritePrivateProfileString(pSection, pKey, null, this.ls_IniFilename);
        }

        //   razygraevaa on 16.09.2022 at 12:01
        /// <summary> ���������� ������ � ������� �� INI File </summary>
        /// <param name="pSection">������</param>
        /// <param name="pKeys">������� ������� ������</param>
        public void ReadKeys(string pSection, ref Array pKeys)
        {
            string _pKeys = z_GetString(pSection, null, null);

            if (!string.IsNullOrEmpty(_pKeys))//���� �� �����
            {
                pKeys = _pKeys.Split((char)0);
            }
            else
            {
                pKeys = null;
            }
            //pValues = z_GetString(pSection, null, null).Split((char)0); by razygraevaa on 07.02.2022 at 12:43
        }

        //   razygraevaa on 16.09.2022 at 12:12
        /// <summary>������ �������� � ������ (�� �������)</summary>
        /// <param name="pSection">������</param>
        /// <param name="pValues">��������</param>
        public void ReadValues(string pSection, ref Array pValues)//think  �������� ����� ReadValues
        {
            //upd �������� ����� ReadValues
            //�������� ��� ����� �� ReadKeys
            //����� � ����� �� ������ �������� ��� ��������
            //������� �������� ref

        }

        /// <summary> ���������� �������� �� INI File</summary>
        /// <param name="pSections">������� ������� ��������</param>
        public void ReadSections(ref Array pSections)
        {
            //   razygraevaa on 07.02.2022 at 12:44
            string _pSections = z_GetString(null, null, null);
            if (!string.IsNullOrEmpty(_pSections))
            {
                pSections = _pSections.Split((char)0);
            }
            else
            {
                pSections = null;
            }
            //pSections = z_GetString(null, null, null).Split((char)0);// by razygraevaa on 07.02.2022 at 12:44
        }



        /// <summary>������� ������ �� INI File</summary>
        /// <param name="pSection">������</param>
        public void RemoveSection(string pSection)
        {
            WritePrivateProfileString(pSection, null, null, this.ls_IniFilename);
        }

        /// <summary> ����� API GetPrivateProfileString / GetPrivateProfileStruct </summary>
        /// <param name="pSection">������</param>
        /// <param name="pKey">����</param>
        /// <param name="pDefault">�������� �� ���������</param>
        /// <returns></returns>
        private string z_GetString(string pSection, string pKey, string pDefault)
        {
            string sRet = pDefault;
            byte[] bRet = new byte[li_BufferLen];
            int i = GetPrivateProfileString(pSection, pKey, pDefault, bRet, li_BufferLen, ls_IniFilename);
            sRet = System.Text.Encoding.GetEncoding(1251).GetString(bRet, 0, i).TrimEnd((char)0);
            return (sRet);
        }
    }
}
