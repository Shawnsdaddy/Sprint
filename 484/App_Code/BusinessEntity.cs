using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusinessEntity
/// </summary>
public class BusinessEntity
{
    private String BusinessName;
    private String PhoneNumber;
    private String BusinessEmail;
    public BusinessEntity(string name, string number, string email)
    {
        setBusinessName(name);
        setPhoneNumber(number);
        setBusinessEmail(email);
    }
    public void setBusinessName(string name)
    {
        this.BusinessName = name;
    }
    public String getBusinessName()
    {
        return this.BusinessName;
    }
    public void setPhoneNumber(string number)
    {
        this.PhoneNumber = number;
    }
    public String getPhoneNumber()
    {
        return this.PhoneNumber;
    }
    public void setBusinessEmail(string email)
    {
        this.BusinessEmail = email;
    }
    public String getBusinessEmail()
    {
        return this.BusinessEmail;
    }
}