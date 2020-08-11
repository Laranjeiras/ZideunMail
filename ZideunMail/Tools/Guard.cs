﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ZideunMail.Tools
{
    public class Guard
    {
        internal const string IsPositiveMessage = "Argument '{0}' must be a positive value. Value: '{1}'.";

        private Guard()
        {
        }
        
        public static void NotNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }
        
        public static void NotEmpty(string arg, string argName)
        {
            if (string.IsNullOrWhiteSpace(arg))
                throw new Exception($"String parameter '{argName}' cannot be null or all whitespace.");
        }
        
        public static void NotEmpty<T>(ICollection<T> arg, string argName)
        {
            if (arg == null || !arg.Any())
                throw new Exception("Collection cannot be null and must contain at least one item.");
        }
        
        public static void NotEmpty(Guid arg, string argName)
        {
            if (arg == Guid.Empty)
                throw new Exception("Argument '{0}' cannot be an empty guid.");
        }
        
        public static void IsPositive<T>(T arg, string argName, string message = IsPositiveMessage) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 1)
                throw new ArgumentOutOfRangeException(argName, argName);
        }
    }
}
