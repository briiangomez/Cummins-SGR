namespace CMM.Data
{
    using CMM;
    using System;

    public static class DataTypeHelper
    {
        public static object DefaultValue(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.String:
                    return "";

                case DataType.Int:
                    return 0;

                case DataType.Decimal:
                    return 0M;

                case DataType.DateTime:
                    return new DateTime();

                case DataType.Bool:
                    return false;
            }
            return null;
        }

        public static object ParseValue(DataType dataType, string value, bool throwWhenInvalid)
        {
            switch (dataType)
            {
                case DataType.String:
                    return value;

                case DataType.Int:
                    int num;
                    if (!int.TryParse(value, out num))
                    {
                        if (throwWhenInvalid)
                        {
                            throw new CMMException("The value is invalid.");
                        }
                        return 0;
                    }
                    return num;

                case DataType.Decimal:
                    decimal num2;
                    if (!decimal.TryParse(value, out num2))
                    {
                        if (throwWhenInvalid)
                        {
                            throw new CMMException("The value is invalid.");
                        }
                        return 0M;
                    }
                    return num2;

                case DataType.DateTime:
                    DateTime time;
                    if (!DateTime.TryParse(value, out time))
                    {
                        if (throwWhenInvalid)
                        {
                            throw new CMMException("The value is invalid.");
                        }
                        return new DateTime();
                    }
                    if (time.Kind != DateTimeKind.Utc)
                    {
                        time = new DateTime(time.Ticks, DateTimeKind.Local).ToUniversalTime();
                    }
                    return time;

                case DataType.Bool:
                    bool flag;
                    if (string.IsNullOrEmpty(value))
                    {
                        return false;
                    }
                    if (!bool.TryParse(value, out flag))
                    {
                        if (throwWhenInvalid)
                        {
                            throw new CMMException("The value is invalid.");
                        }
                        return false;
                    }
                    return flag;
            }
            return null;
        }
    }
}

