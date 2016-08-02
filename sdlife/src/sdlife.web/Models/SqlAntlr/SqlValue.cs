using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models.SqlAntlr
{
    public struct SqlValue
    {
        private double? Number;

        private string String;

        private DateTime? Date;

        public SqlValues ValueType;

        public object Value()
        {
            if (ValueType == SqlValues.Number)
            {
                return Number.Value;
            }
            else if (ValueType == SqlValues.String)
            {
                return String;
            }
            else if (ValueType == SqlValues.Date)
            {
                return Date;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(ValueType));
            }
        }

        public override string ToString()
        {
            if (ValueType == SqlValues.Number)
            {
                return Number.Value.ToString();
            }
            else if (ValueType == SqlValues.String)
            {
                return '"' + String + '"';
            }
            else if (ValueType == SqlValues.Date)
            {
                return Date.Value.ToString();
            }
            throw new ArgumentOutOfRangeException(nameof(ValueType));
        }

        public static SqlValue ParseNumber(string text)
        {
            return new SqlValue
            {
                ValueType = SqlValues.Number,
                Number = double.Parse(text)
            };
        }

        public static SqlValue ParseNumber(double v)
        {
            return new SqlValue
            {
                ValueType = SqlValues.Number,
                Number = v
            };
        }

        public static SqlValue ParseDate(string text)
        {
            return new SqlValue
            {
                ValueType = SqlValues.Date,
                Date = DateTime.Parse(text)
            };
        }

        public static explicit operator double(SqlValue v)
        {
            return v.Number.Value;
        }

        public static explicit operator string(SqlValue v)
        {
            if (v.String == null)
                throw new NullReferenceException();
            return v.String;
        }

        public static explicit operator DateTime(SqlValue v)
        {
            return v.Date.Value;
        }

        public static implicit operator SqlValue(double v)
        {
            return ParseNumber(v);
        }

        public static implicit operator SqlValue(string v)
        {
            if (v == null)
                throw new NullReferenceException();

            return new SqlValue
            {
                ValueType = SqlValues.String,
                String = v
            };
        }

        public static implicit operator SqlValue(DateTime v)
        {
            return new SqlValue
            {
                ValueType = SqlValues.Date,
                Date = v
            };
        }

        public static SqlValue operator +(SqlValue v1, SqlValue v2)
        {
            if (v1.ValueType == v2.ValueType)
            {
                if (v1.ValueType == SqlValues.Number)
                {
                    return (double)v1 + (double)v2;
                }
                else if (v1.ValueType == SqlValues.String)
                {
                    return (string)v1 + (string)v2;
                }
            }
            throw new NotSupportedException();
        }

        public static SqlValue operator -(SqlValue v1, SqlValue v2)
        {
            if (v1.ValueType == v2.ValueType)
            {
                if (v1.ValueType == SqlValues.Number)
                {
                    return (double)v1 - (double)v2;
                }
            }
            throw new NotSupportedException();
        }
    }
}
