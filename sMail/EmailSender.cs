using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace sMail
{
    public class EmailSender
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SMTP_HOST = "mx.info.wisol.co.kr";
        private const int SMTP_PORT = 25;
        public string SMTP_ID = "dhshin";
        public string SMTP_PW = "dhshin";
        public string FROM_ADDRESS = "thidao335@wisol.co.kr";
        private const string FROM_NAME = "WHC_HR";

        private List<string> _toEmailAddressList = new List<string>();
        private List<string> _ccEmailAddressList = new List<string>();
        private string _subject = string.Empty;
        private string _body = string.Empty;
        private List<string> _attachmentFilePathList = new List<string>();
        private List<Attachment> _attachmentFileDataList = new List<Attachment>();

        public EmailSender() { }


        /// <summary>
        /// Add To Email Address
        /// </summary>
        /// <param name="toEmailAddress"></param>
        public void AddToEmailAddress(string toEmailAddress)
        {
            _toEmailAddressList.Add(toEmailAddress);
        }


        /// <summary>
        /// Add Range To Email Address
        /// </summary>
        /// <param name="toEmailAddressList"></param>
        public void AddRangeToEmailAddress(string[] toEmailAddressList)
        {
            foreach (string toEmailAddress in toEmailAddressList)
            {
                AddToEmailAddress(toEmailAddress);
            }
        }


        /// <summary>
        /// Add Carbon Copy Email Address
        /// </summary>
        /// <param name="ccEmailAddress"></param>
        public void AddCcEmailAddress(string ccEmailAddress)
        {
            _ccEmailAddressList.Add(ccEmailAddress);
        }


        /// <summary>
        /// Add Range Carbon Copy Email Address
        /// </summary>
        /// <param name="ccEmailAddressList"></param>
        public void AddRangeCcEmailAddress(string[] ccEmailAddressList)
        {
            foreach (string ccEmailAddress in ccEmailAddressList)
            {
                AddCcEmailAddress(ccEmailAddress);
            }
        }


        /// <summary>
        /// Add Attachment File Path
        /// </summary>
        /// <param name="attachmentFilePath"></param>
        public void AddAttachmentFilePath(string attachmentFilePath)
        {
            _attachmentFilePathList.Add(attachmentFilePath);
        }

        public void AddAttachmentData(Attachment data)
        {
            _attachmentFileDataList.Add(data);
        }

        /// <summary>
        /// Add Range Attachment File Path
        /// </summary>
        /// <param name="attachmentFilePathList"></param>
        public void AddRangeAttachmentFilePath(string[] attachmentFilePathList)
        {
            foreach (string attachmentFilePath in attachmentFilePathList)
            {
                AddAttachmentFilePath(attachmentFilePath);
            }
        }


        /// <summary>
        /// Email Subject
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _subject = value;
            }
        }


        /// <summary>
        /// Email Body
        /// </summary>
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _body = value;
            }
        }


        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="ExceptionMessage"></param>
        /// <returns></returns>
        public bool Send()
        {
            try
            {
                if (_toEmailAddressList == null || _toEmailAddressList.Count == 0)
                {
                    return false;
                }

                foreach (string _toEmailAddress in _toEmailAddressList)
                {
                    if (String.IsNullOrWhiteSpace(_toEmailAddress))
                    {
                        return false;
                    }

                    int atIndex = _toEmailAddress.IndexOf('@');
                    if (atIndex == -1)
                    {
                        return false;
                    }
                }

                foreach (string _ccEmailAddress in _ccEmailAddressList)
                {
                    int atIndex = _ccEmailAddress.IndexOf('@');
                    if (atIndex == -1)
                    {
                        return false;
                    }
                }

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FROM_ADDRESS, FROM_NAME);

                // Add To Email Address
                foreach (string _toEmailAddress in _toEmailAddressList)
                {
                    mail.To.Add(_toEmailAddress);
                }

                // Add Carbon Copy Email Address
                foreach (string _ccEmailAddress in _ccEmailAddressList)
                {
                    mail.CC.Add(_ccEmailAddress);
                }
                mail.Subject = _subject;
                mail.Body = _body;
                if (_attachmentFilePathList != null && _attachmentFilePathList.Count > 0)
                {
                    foreach (string _attachmentFilePath in _attachmentFilePathList)
                    {
                        Attachment attachmentFile = new Attachment(_attachmentFilePath, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachmentFile.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(_attachmentFilePath);
                        disposition.ModificationDate = File.GetLastWriteTime(_attachmentFilePath);
                        disposition.ReadDate = File.GetLastAccessTime(_attachmentFilePath);
                        disposition.FileName = Path.GetFileName(_attachmentFilePath);
                        disposition.Size = new FileInfo(_attachmentFilePath).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;

                        mail.Attachments.Add(attachmentFile);
                    }
                }

                if (_attachmentFileDataList != null && _attachmentFileDataList.Count > 0)
                {
                    foreach (var item in _attachmentFileDataList)
                    {
                        mail.Attachments.Add(item);
                    }
                }

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(SMTP_HOST, SMTP_PORT);
                smtp.Credentials = new System.Net.NetworkCredential(SMTP_ID, SMTP_PW);
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog("Send error1 :" + ex.Message);
                WriteLogFile.WriteLog("Send error2 :" + ex.StackTrace);
                return false;
            }
        }
    }
}
