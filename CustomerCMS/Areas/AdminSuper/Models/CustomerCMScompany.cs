using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomerCMS.Areas.AdminSuper.Models
{
    public class CustomerCMScompany
    {
        /// <summary>
        /// 公司唯一主键
        /// </summary>
        [DisplayName("主键")]
        public int ID { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string cc_account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [DisplayName("登录密码")]
        public string cc_password { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DisplayName("公司名称")]
        public string cc_name { get; set; }
        /// <summary>
        /// 公司营业执照号
        /// </summary>
        [DisplayName("营业执照号")]
        public string cc_numer { get; set; }
        /// <summary>
        /// 营业执照图片地址
        /// </summary>
        [DisplayName("营业执照图片")]
        public string cc_img { get; set; }
        /// <summary>
        /// 法人姓名
        /// </summary>
        [DisplayName("法人姓名")]
        public string cc_fr { get; set; }
        /// <summary>
        /// 外置邮件，还是内部编辑邮件 0 自定义富文本编辑邮件 1外置链接自定义网页
        /// </summary>
        [DisplayName("邮件类型")]
        public int cc_emailsetting { get; set; }
        /// <summary>
        /// 0启用每天回收资源 1启用指定时间回收资源
        /// </summary>
        [DisplayName("回收方式")]
        public int cc_revoerytime { get; set; }
        /// <summary>
        /// 只有当cc_revoerytime=0时，每天都在这个时间回收
        /// </summary>
        [DisplayName("每天回收")]
        public string cc_revoerytime_everday { get; set; }
        /// <summary>
        /// 只有当cc_revoerytime=1时在这个指定的时间回收一次
        /// </summary>
        [DisplayName("一次回收")]
        public DateTime cc_revoerytime_oneday{ get; set; }
        /// <summary>
        /// 公司注册时间
        /// </summary>
        [DisplayName("注册时间")]
        public DateTime cc_regtime { get; set; }
        /// <summary>
        /// 公司到期时间
        /// </summary>
        [DisplayName("到期时间")]
        public DateTime cc_endtime { get; set; }
        /// <summary>
        /// 0被删除，1待激活，2激活，3违规被禁用，4到期
        /// </summary>
        [DisplayName("公司状态")]
        public int cc_flag { get; set; }
    }
}