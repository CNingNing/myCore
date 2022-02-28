﻿namespace Winner.Message
{
    public class MessageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 失效时间秒
        /// </summary>
        public int ExpireSecond { get; set; } = 3600;
    }
}
