using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RewardsProvider
{
    private string companyName;
    private string typeOfBusiness;
    private string email;
    private string LastUpdatedBy;
    private DateTime LastUpdated;
    //private int amountprovided;
    public RewardsProvider(string companyName, string email, string typeOfBusiness, DateTime LastUpdated, string LastUpdatedBy)
    {
        setcompnayName(companyName);
        setemail(email);
        settypeOfBusiness(typeOfBusiness);
        setLastUpdated(LastUpdated);
        setLastUpdatedBy(LastUpdatedBy);
        //setAmountProvided(amountprovided);  
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
    //public void setAmountProvided(int amountprovided)
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
    //public double getamountProvided()
    //{
    //    return this.amountprovided;
    //}
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
}