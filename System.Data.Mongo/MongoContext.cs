﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace System.Data.Mongo
{
    public class MongoContext
    {
        /// <summary>
        /// This indicates if the context should load properties 
        /// that are not part of a given class definition into a 
        /// special flyweight lookup. 
        /// 
        /// Disabled by default.
        /// </summary>
        /// <remarks>
        /// This is useful when the class definition you want to use doesn't support a particular property, but the database should 
        /// still maintain it, or you do not want to squash it on save.
        /// 
        /// Enabling this will cause additinal overhead when loading/saving, as well as more memory consumption during the lifetime of the object.
        /// </remarks>
        public bool EnableExpandoProperties
        {
            get;
            protected set;
        }

        /// <summary>
        /// Number of seconds to wait for a response from the server before throwing a timeout exception.
        /// Defaults to 30.
        /// </summary>
        public int QueryTimeout
        {
            get;
            set;
        }
        /// <summary>
        /// The ip/domain name of the server.
        /// </summary>
        protected String _serverName = "127.0.0.1";
        /// <summary>
        /// The port on which the server is accessible.
        /// </summary>
        protected int _serverPort = 27017;

        protected IPEndPoint _endPoint;

        public MongoContext()
        {
            this.QueryTimeout = 30;
            var entry = Dns.GetHostEntry(this._serverName);
            var ipe = entry.AddressList.First();
            this._endPoint = new IPEndPoint(ipe, this._serverPort);
        }

        /// <summary>
        /// Will provide an object reference to a DB within the current context.
        /// </summary>
        /// <remarks>
        /// I would recommend adding extension methods, or subclassing MongoContext 
        /// to provide strongly-typed members for each database in your context, this
        /// will removed strings except for a localized places - which can should 
        /// reduce typo problems..
        /// </remarks>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public MongoDatabase GetDatabase(String dbName)
        {
            var retval = new MongoDatabase(dbName, this);
            return retval;
        }

        /// <summary>
        /// Constructs a socket to the server.
        /// </summary>
        /// <returns></returns>
        internal Socket Socket()
        {
            Socket sock = new Socket(this._endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(this._serverName, this._serverPort);
            return sock;
        }

        /// <summary>
        /// Returns a list of databases that already exist on this context.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetAllDatabases()
        {


            yield break;
        }


    }
}
