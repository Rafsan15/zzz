﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS_Entities;
using FMS_Framework.Helper;
using FMS_Framework.Object;
using WorkerInfo = FMS_Entities.WorkerInfo;
namespace FMS_Repository_EF
    { 
    class WorkerInfoRepo:BaseRepo
    {

        
            public Result<WorkerInfo> Save(WorkerInfo userinfo)
            {
                var result = new Result<WorkerInfo>();
                try
                {
                    var objtosave = DbContext.WorkerInfos.FirstOrDefault(u => u.UserId == userinfo.UserId);
                    if (objtosave == null)
                    {
                        objtosave = new WorkerInfo();
                        DbContext.WorkerInfos.Add(objtosave);
                    }
                    objtosave.EarnedMoney = userinfo.EarnedMoney;
                    objtosave.RatePerHour = userinfo.RatePerHour;
                    


                    if (!IsValid(objtosave, result))
                    {
                        return result;
                    }
                    DbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Message = ex.Message;
                }
                return result;
            }

            private bool IsValid(WorkerInfo obj, Result<WorkerInfo> result)
            {
                if (!ValidationHelper.IsStringValid(obj.EarnedMoney.ToString()))
                {
                    result.HasError = true;
                    result.Message = "Invalid EarnedMoney";
                    return false;
                }
                if (!ValidationHelper.IsStringValid(obj.RatePerHour))
                {
                    result.HasError = true;
                    result.Message = "Invalid RatePerHour";
                    return false;
                }


                return true;
            }

            public Result<List<WorkerInfo>> GetAll(string key = "")
            {
                var result = new Result<List<WorkerInfo>>() { Data = new List<WorkerInfo>() };

                try
                {
                    IQueryable<WorkerInfo> query = DbContext.WorkerInfos;

                    if (ValidationHelper.IsIntValid(key))
                    {
                        query = query.Where(q => q.UserId == Int32.Parse(key));
                    }

                    if (ValidationHelper.IsStringValid(key))
                    {
                        query = query.Where(q => q.EarnedMoney.Equals(Int32.Parse(key)));

                    }

                    if (ValidationHelper.IsStringValid(key))
                    {
                        query = query.Where(q => q.RatePerHour.Contains(key));

                    }

                    
                    result.Data = query.ToList();
                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Message = e.Message;


                }
                return result;
            }

            public Result<WorkerInfo> GetByID(int id)
            {
                var result = new Result<WorkerInfo>();

                try
                {
                    var obj = DbContext.WorkerInfos.FirstOrDefault(c => c.UserId == id);
                    if (obj == null)
                    {
                        result.HasError = true;
                        result.Message = "Invalid UserID";
                        return result;


                    }
                    result.Data = obj;
                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Message = e.Message;


                }
                return result;
            }

            public Result<bool> Delete(int id)
            {
                var result = new Result<bool>();

                try
                {
                    var objtodelete = DbContext.WorkerInfos.FirstOrDefault(c => c.UserId == id);
                    if (objtodelete == null)
                    {
                        result.HasError = true;
                        result.Message = "Invalid UserID";
                        return result;


                    }

                    DbContext.WorkerInfos.Remove(objtodelete);
                    DbContext.SaveChanges();

                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Message = e.Message;


                }
                return result;
            }

            /*   private bool  IsValidToSave(WorkerInfo obj, Result<WorkerInfo> result)
               {
                   if(!ValidationHelper.IsIntValid(obj.UserId))
                   {
                       result.HasError = true;
                       result.Message = "Invalid UserID";
                       return false;

                   }
                   if (DbContext.WorkerInfos.Any(u =>
                       u.UserId == obj.UserId && u.School != obj.School && u.Collage != obj.Collage &&
                       u.UniversityPost != obj.UniversityPost && u.UniversityUnder != obj.UniversityUnder &&
                       u.Others != obj.Others))
                   {
                
            

                       result.HasError= true;
                       result.Message = "UserID Exists";
                       return false;



                   }
                   return true;

               }*/



        }
    }


