using System;
using System.ComponentModel;
using System.Reflection;

namespace AspBookLibrary.Extensions
{
    public enum RoleTypes
    {
        [Description("Member")]
        Member,
        [Description("Moderator")]
        Moderator,
        [Description("Administrator")]
        Administrator
    }

    public static class RoleExtention
    {
        public static string Get(this RoleTypes enumerationValue)
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0) return enumerationValue.ToString();
            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : enumerationValue.ToString();
            //If we have no description attribute, just return the ToString of the enum
        }
    }
}