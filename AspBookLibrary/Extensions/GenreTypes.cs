using System;
using System.ComponentModel;
using System.Reflection;

namespace AspBookLibrary.Extensions
{
    public enum GenreTypes
    {
        [Description("Member")]
        Fiction,
        [Description("Comedy")]
        Comedy,
        [Description("Drama")]
        Drama,
        [Description("Horror")]
        Horror,
        [Description("Non-Fiction")]
        NonFiction,
        [Description("Realistic Fiction")]
        RealisticFiction,
        [Description("Romance Novel")]
        RomanceNovel,
        [Description("Satire")]
        Satire,
        [Description("Tragedy")]
        Tragedy,
        [Description("Tragicomedy")]
        Tragicomedy,
        [Description("Fantasy")]
        Fantasy,
        [Description("Mythology")]
        Mythology,
    }

    public static class GenreExtention
    {
        public static string Get(this GenreTypes enumerationValue)
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