﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CustomerCMS.Controllers
{
    public class UploaderController : Controller
    {
        // GET: Uplpader
        public ActionResult Index()
        {
            return View();
        }
        private string FilePhysicalPath = string.Empty;
        private string FileRelativePath = string.Empty;
        #region Private Methods
        /// <summary>
        /// 检查是否为合法的上传图片
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private string CheckImage(HttpPostedFileBase imgfile)
        {
            string allowExt = ".gif.jpg.png";
            string fileName = imgfile.FileName;
            FileInfo file = new FileInfo(fileName);
            string imgExt = file.Extension;
            Image img = IsImage(imgfile);
            string errorMsg = fileName + "：";
            if (img == null)
            {
                errorMsg = "文件格式错误，请上传gif、jpg、png格式的图片";
                return errorMsg;
            }
            if (allowExt.IndexOf(imgExt.ToLower()) == -1)
            {
                errorMsg = "请上传gif、jpg、png格式的图片；";
            }
            //if (imgfile.ContentLength > 512 * 1024)
            //{
            //    errorMsg += "图片最大限制为0.5Mb；";
            //}
            //if (img.Width < 20 || img.Width > 480 || img.Height < 20 || img.Height > 854)
            //{
            //    errorMsg += "请上传正确尺寸的图片，图片最小为20x20，最大为480*854。";
            //}
            if (errorMsg == fileName + "：")
            {
                return "";
            }
            return errorMsg;

        }

        /// <summary>
        /// 验证是否为真正的图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private Image IsImage(HttpPostedFileBase file)
        {
            try
            {
                Image img = Image.FromStream(file.InputStream);
                return img;
            }
            catch
            {
                return null;
            }
        }

        private string NewFileName(string fileNameExt)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileNameExt;
            //return Guid.NewGuid().ToString() + fileNameExt;
        }

        private UploadImgResult UploadImage(HttpPostedFileBase file)
        {
            UploadImgResult result = new UploadImgResult();
            try
            {
                string fileNameExt = (new FileInfo(file.FileName)).Extension;

                //string saveName = GetImageName() + fileNameExt;

                //获得要保存的文件路径
                String newFileName = NewFileName(fileNameExt);
                String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                FilePhysicalPath += ymd + "/";
                FileRelativePath += ymd + "/";
                String fileFullPath = FilePhysicalPath + newFileName;
                if (!Directory.Exists(FilePhysicalPath))
                    Directory.CreateDirectory(FilePhysicalPath);
                file.SaveAs(fileFullPath);

                string relativeFileFullPath = FileRelativePath + newFileName;
                result.Result = 1;
                result.msg = relativeFileFullPath;
            }
            catch (Exception ex)
            {
                result.Result = 3;
                result.msg = ex.Message;
            }
            return result;
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonConvert.SerializeObject(hash));
            Response.End();
        }
        #endregion

        [HttpPost]
        public string UploadPic()
        {
            Response.ContentType = "text/plain";
            List<UploadImgResult> results = new List<UploadImgResult>();
            FileRelativePath = System.Configuration.ConfigurationManager.AppSettings["UploadProductImgPath"];
            FilePhysicalPath = System.Web.HttpContext.Current.Server.MapPath(FileRelativePath);
            if (!Directory.Exists(FilePhysicalPath))
            {
                Directory.CreateDirectory(FilePhysicalPath);
            }

            string saveFileResult = string.Empty;
            HttpFileCollectionBase files = (HttpFileCollectionBase)Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                UploadImgResult result = null;
                string checkResult = CheckImage(file);
                if (string.IsNullOrEmpty(checkResult))
                {
                    result = UploadImage(file);
                }
                else
                {
                    result = new UploadImgResult();
                    result.Result = 2;
                    result.msg = checkResult;
                }
                results.Add(result);
            }
            string json = JsonConvert.SerializeObject(results);
            return json;
        }


        private void HandleWaterMark(string fileFullPath)
        {
            bool isWaterMask = Request.Form["watermask"] == "1";
            if (isWaterMask)
            {
                string waterurl = Request.MapPath("/Content/water.png");
                //CommonHelper.MakeWatermark(fileFullPath, waterurl, fileFullPath, 4, 95);
            }
        }

        public string FileManagerJson()
        {
            String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

            ////根目录路径，相对路径
            //String rootPath = "../attached/";
            ////根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            //String rootUrl = aspxUrl + "../attached/";

            //根目录路径，相对路径
            String rootPath = System.Configuration.ConfigurationManager.AppSettings["UploadDir"];
            //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
            String rootUrl = System.Configuration.ConfigurationManager.AppSettings["UploadDir"];

            //图片扩展名
            String fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath = "";
            String currentUrl = "";
            String currentDirPath = "";
            String moveupDirPath = "";

            String dirPath = Server.MapPath(rootPath);
            String dirName = Request.QueryString["dir"];
            if (!String.IsNullOrEmpty(dirName))
            {
                if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                {
                    return "Invalid Directory name.";
                }
                dirPath += dirName + "/";
                rootUrl += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }

            //根据path参数，设置各路径和URL
            String path = Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            {
                Response.Write("Access is not allowed.");
                Response.End();
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            {
                Response.Write("Parameter is not valid.");
                Response.End();
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath))
            {
                Response.Write("Directory does not exist.");
                Response.End();
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            Hashtable result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            List<Hashtable> dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            for (int i = 0; i < dirList.Length; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(dirList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                FileInfo file = new FileInfo(fileList[i]);
                Hashtable hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            //Response.Write(JsonConvert.SerializeObject(result));
            //Response.End();
            return JsonConvert.SerializeObject(result);
        }

    }

    #region Model
    public class UploadImgResult
    {
        public int Result { set; get; } //1成功，2失败，3异常
        public string msg { set; get; }
    }
    public class NameSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.FullName.CompareTo(yInfo.FullName);
        }
    }

    public class SizeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Length.CompareTo(yInfo.Length);
        }
    }

    public class TypeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Extension.CompareTo(yInfo.Extension);
        }
    }
    #endregion
}