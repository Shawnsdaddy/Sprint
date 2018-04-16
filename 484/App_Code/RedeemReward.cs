using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RedeemReward
{

    private string itemName;
    private DateTime dateRedeemed;
    private double transactionAmount;
    private string lastUpdatedBy;
    private DateTime lastupdated;
    private double redeemAmount;
    private double redeemQuantity;
    public RedeemReward(DateTime dateRedeemed,double redeemAmount, double redeemQuantity, double transactionAmount, DateTime lastUpdated, string lastUpdatedBy)
    {
        setDateRedeemed(dateRedeemed);
        setRedeemAmount(redeemAmount);
        setRedeemQuantity(redeemQuantity);
        setTransactionAmount(transactionAmount);
        setlastUpdatedBy(lastUpdatedBy);
        setlastUpdated(lastUpdated);
    }
 
    public void setDateRedeemed(DateTime dateRedeemed)
    {
        this.dateRedeemed = dateRedeemed;
    }
    public void setRedeemAmount(double redeemAmount)
    {
        this.redeemAmount = redeemAmount;
    }
    public void setRedeemQuantity(double redeemQuantity)
    {
        this.redeemQuantity = redeemQuantity;
    }
    public void setItem(string itemName)
    {
        this.itemName = itemName;
    }
public void setTransactionAmount (double transactionAmount)
    {
        this.transactionAmount = this.redeemAmount*this.redeemQuantity;
    }
    public void setlastUpdatedBy(string lastUpdatedBy)
    {
        this.lastUpdatedBy = lastUpdatedBy;
    }
    public void setlastUpdated(DateTime lastUpdated)
    {
        this.lastupdated = lastUpdated;
    }

    public DateTime getDate()
    {
        return this.dateRedeemed;
    }
    public string getItem()
    {
        return this.itemName;
    }
public double getTransactionAmount()
    {
        return this.transactionAmount;
    }

    public double getRedeemAmount()
    {
        return this.redeemAmount;
    }

    public double getRedeemQuantity()
    {
        return this.redeemQuantity;
    }
    public String getlastUpdatedBy()
    {
        return this.lastUpdatedBy;
    }
    public DateTime getlastUpdated()
    {
        return this.lastupdated;
    }
}