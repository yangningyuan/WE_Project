﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace WE_Project.BLL
{
    //MGetHelp
    public class MGetHelp
    {
        public static Model.MGetHelp GetModel(object obj)
        {
            return WE_Project.DAL.MGetHelp.GetModel(obj);
        }

        public static Hashtable Insert(Model.MGetHelp model, Hashtable MyHs)
        {
            return WE_Project.DAL.MGetHelp.Insert(model, MyHs);
        }

        public static bool Insert(Model.MGetHelp model)
        {
            return WE_Project.DAL.MGetHelp.Insert(model);
        }

        public static Hashtable Update(Model.MGetHelp model, Hashtable MyHs)
        {
            return WE_Project.DAL.MGetHelp.Update(model, MyHs);
        }

        public static bool Update(Model.MGetHelp model)
        {
            return WE_Project.DAL.MGetHelp.Update(model);
        }

        public static Hashtable Delete(object obj, Hashtable MyHs)
        {
            return WE_Project.DAL.MGetHelp.Delete(obj, MyHs);
        }

        public static bool Delete(object obj)
        {
            return WE_Project.DAL.MGetHelp.Delete(obj);
        }

        public static DataTable GetTable(string strWhere)
        {
            return WE_Project.DAL.MGetHelp.GetTable(strWhere);
        }

        public static DataTable GetTable(string strWhere, int pageIndex, int pageSize, out int count)
        {
            return WE_Project.DAL.MGetHelp.GetTable(strWhere, pageIndex, pageSize, out count);
        }

        public static List<Model.MGetHelp> GetList(string strWhere)
        {
            return WE_Project.DAL.MGetHelp.GetList(strWhere);
        }
        public static List<Model.MGetHelp> GetListJoinMember(string strWhere)
        {
            return WE_Project.DAL.MGetHelp.GetListJoinMember(strWhere);
        }

        public static List<Model.MGetHelp> GetList(string strWhere, int pageIndex, int pageSize, out int count)
        {
            return WE_Project.DAL.MGetHelp.GetList(strWhere, pageIndex, pageSize, out count);
        }

        public static string GetSumCount(string type)
        {
            string sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp ";
            switch (type)
            {
                case "": sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp "; break;
                case "1": sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp  where PPState in (0,2)"; break;
                case "2": sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp where PPState=1"; break;
                case "3": sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp  where PPState=3"; break;
                case "4": sql = "select COUNT(1),ISNULL(SUM(SQMoney),0) from MGetHelp  where PPState=1"; break;
            }
            DataTable list = BLL.CommonBase.GetTable(sql);
            return list.Rows[0][0] + "*" + list.Rows[0][1];
        }

        public static decimal GetSumMoney(string strWhere)
        {
            return DAL.MGetHelp.GetSumMoney(strWhere);
        }

        /// <summary>
        /// 获得帮助
        /// </summary>
        public static string GetHelp(Model.Member member, string moneyType, decimal sqMoney, Hashtable MyHs, bool isMoneyChcek = true)
        {
            if (!member.MState)
            {
                return "1*您的账号未激活，请先激活账号！";
            }
            if (member.Role.IsAdmin)
            {
                return "1*不可申请";
            }

            //获得帮助开关
            if (!BLL.MMMConfig.Model.GetHelpSwitch || !SystemTimeRange.TimeIsClose(BLL.MMMConfig.Model.GetHelpTimes, DateTime.Now))
            {
                return "1*当前时间不能申请";
            }

            int getcount= Convert.ToInt32( BLL.CommonBase.GetSingle("SELECT COUNT(*) FROM MGetHelp WHERE SQMID='"+member.MID+"' AND PPState<3;"));
            if (getcount > 0)
            {
                return "1*您有未完成的订单";
            }

            if (sqMoney % BLL.MMMConfig.Model.GetHelpBase != 0)
            {
                return "1*提现金额应为" + BLL.MMMConfig.Model.GetHelpBase + "的倍数";
            }
            if (sqMoney >BLL.MMMConfig.Model.GetHelpMax || sqMoney < BLL.MMMConfig.Model.GetHelpMin)
            {
                return "1*提现失败，提现范围应在" + BLL.MMMConfig.Model.GetHelpMin + "-" + BLL.MMMConfig.Model.GetHelpMax;
            }


            //校验援助次数
            //if (!CanApplyHelp(member.MID, BLL.MMMConfig.Model.GetHelpRangeTimes, BLL.MMMConfig.Model.GetHelpRangeCount, false, moneyType))
            //{
            //    return "1*申请失败，您" + BLL.MMMConfig.Model.GetHelpRangeTimes + "分钟内只能申请" + BLL.MMMConfig.Model.GetHelpRangeCount + "次";
            //}

            //如果是动态奖金就限制额度
            if (moneyType == "MJB") 
            {
                decimal mjbsum = BLL.MGetHelp.GetSumMoney("  SQMID='" + member.MID + "' and PPState<=3 and MoneyType='MJB';  ");
                
                decimal mjbedu=member.MConfig.SHMoney * BLL.MMMConfig.Model.GetHelpFloat;
                if ((mjbsum + sqMoney) >mjbedu )
                    return "1*动态奖金提现额度不够，您还能提现" + (mjbedu - mjbsum);

                //动态奖金消耗排单币
                Model.ConfigDictionary dicmcw= DAL.ConfigDictionary.GetConfigDictionary(Convert.ToInt32(sqMoney), "MGetMGP", "");
                decimal getmgp = Convert.ToDecimal(dicmcw.DValue) * Convert.ToDecimal(sqMoney);
                if(!BLL.ChangeMoney.EnoughChange(member.MID,getmgp,"MGP"))
                    return "1*提现失败，排单币不足";
                else
                    BLL.ChangeMoney.HBChangeTran(getmgp, member.MID, BLL.Member.ManageMember.TModel.MID, "TXBH", member, "MGP", "获得帮助需" + getmgp + "个排单币", MyHs);
            }


            //校验马夫罗是否足够
            if (!isMoneyChcek || BLL.ChangeMoney.EnoughChange(member.MID, sqMoney, moneyType))
            {
                Model.MGetHelp get = new Model.MGetHelp();
                get.ConfirmState = 0;
                //get.MFLMoney = sqMoney;
                get.PPState = 0;
                get.MoneyType = moneyType;
                //get.SQCode = Guid.NewGuid().ToString("N").ToUpper();
                get.SQDate = DateTime.Now;
                get.SQMID = member.MID;
                get.SQMoney = sqMoney;
                get.ComfirmDate = DateTime.MaxValue;
                //get.Remark = Request.Form["BankCode"];
                //Hashtable hs = new Hashtable();
                BLL.MGetHelp.Insert(get, MyHs);
                //扣除马夫罗
                BLL.ChangeMoney.HBChangeTran(get.SQMoney, member.MID, BLL.Member.ManageMember.TModel.MID, "GET", null, moneyType, member.MID + "申请获得" + get.SQMoney + "的帮助", MyHs);
                return "1*0";
                //if (BLL.CommonBase.RunHashtable(MyHs))
                //{
                //    return "1*0";
                //}
                //else
                //{
                //    return "1*提交申请失败";
                //}
            }
            else
            {
                return "1*您的" + BLL.Reward.List[moneyType].RewardName + "余额不足";
            }
            //}
        }

        /// <summary>
        /// 获得帮助
        /// </summary>
        public static string GetHelp(Model.Member member, string moneyType, decimal sqMoney, bool isMoneyChcek = true)
        {
            Hashtable MyHs = new Hashtable();
            string result = GetHelp(member, moneyType, sqMoney, MyHs, isMoneyChcek);
            if (result == "1*0")
            {
                if (BLL.CommonBase.RunHashtable(MyHs))
                {
                    return "1*0";
                }
                else
                {
                    return "1*提交申请失败";
                }
            }
            else
            {
                return result;
            }
        }

        private static string MoneyRange(Model.Member member, string moneyType, decimal sqMoney, int moneyBase, decimal maxGetMoney, decimal minGetMoney, int rangeTimes, int rangeCount)
        {
            //MHB互助钱包,100的倍数,一天一次
            if (sqMoney % moneyBase != 0)
            {
                return "1*援助金额应为" + moneyBase + "的倍数";
            }

            if (sqMoney < minGetMoney || sqMoney > maxGetMoney)
            {
                return "1*提交申请失败：申请金额应该为：" + minGetMoney + "-" + maxGetMoney;
            }

            //校验援助次数
            if (!CanApplyHelp(member.MID, BLL.MMMConfig.Model.GetHelpRangeTimes, BLL.MMMConfig.Model.GetHelpRangeCount, false, moneyType))
            {
                return "1*申请失败，您" + BLL.MMMConfig.Model.GetHelpRangeTimes + "分钟内只能用" + BLL.Reward.List[moneyType].RewardName + "申请" + BLL.MMMConfig.Model.GetHelpRangeCount + "次";
            }
            return "";
        }

        /// <summary>
        /// 是否可以申请(指定时间指定数量)
        /// </summary>
        protected static bool CanApplyHelp(string mid, int time, int count, bool isOffer, string moneyType)
        {
            string tableName = "MOfferHelp";
            if (!isOffer)
            {
                tableName = "MGetHelp";
            }
            DateTime now = DateTime.Now.AddMinutes(-time);
            string sql = string.Format(" select COUNT(*) from {0} where PPState <> 5 and SQMID = '{1}' and SQDate > '{2}' {3} ", tableName, mid, now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "");
            object obj = BLL.CommonBase.GetSingle(sql);
            if (obj != null)
            {
                return Convert.ToInt32(obj) < count;
            }
            else
            {
                return 0 < count;
            }
        }
    }
}