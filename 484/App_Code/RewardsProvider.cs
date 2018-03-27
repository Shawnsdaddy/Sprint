using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RewardsProvider
{
    private string companyName;
    private string typeOfBusiness;
    private string email;
    private int quantity;
    private string LastUpdatedBy;
    private DateTime LastUpdated;
    private double amountprovided;
    private int BusinessEntityID;
    public RewardsProvider(string companyName, string email, string typeOfBusiness, DateTime LastUpdated, string LastUpdatedBy, int BusinessEntityID)
    {
        setcompnayName(companyName);
        setemail(email);
        settypeOfBusiness(typeOfBusiness);
        setLastUpdated(LastUpdated);
        setLastUpdatedBy(LastUpdatedBy);
        amountprovided = 20;
        setBusinessEntityID(BusinessEntityID);
       
    }
    public void setcompnayName(string companyname)
    {
        this.companyName = companyname;
    }
    public void settypeOfBusiness(string typeOfBusiness)
    {
        this.typeOfBusiness = typeOfBusiness;
    }
    
    public void setemail(string email)
    {
        this.email = email;
    }
    //public void setamountProvided(double amountprovided)
    //{
    //    this.amountprovided = amountprovided;
    //}
    
    public string getcompanyName()
    {
        return this.companyName;
    }
    public string gettypeOfBusiness()
    {
        return this.typeOfBusiness;
    }
    public string getemail()
    {
        return this.email;
    }
    public double getamountProvided()
    {
        return this.amountprovided;
    }
    public void setLastUpdatedBy(string lastUpdatedBy)
    {
        this.LastUpdatedBy = lastUpdatedBy;
    }
    public void setLastUpdated(DateTime lastUpdated)
    {
        this.LastUpdated = lastUpdated;
    }
    public string getLastUpdatedBy()
    {
        return this.LastUpdatedBy;
    }
    public DateTime getLastUpdated()
    {
        return this.LastUpdated;
    }

    public void setBusinessEntityID(int BusinessEntityID)
    {
        this.BusinessEntityID = BusinessEntityID;
    }

    public int getBusinessEntityID()
    {
        return this.BusinessEntityID;
    }
}