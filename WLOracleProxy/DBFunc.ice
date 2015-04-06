// **********************************************************************
//
// Copyright (c) 2003-2011 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

#ifndef DBFUNC_ICE
#define DBFUNC_ICE

module Demo
{

interface DBFunc
{
    void queryOne(string strSql);
    string queryDS(string strSql);
    int query(string strSql);
    int batchInsert(string data);
    void shutdown();
};

};

#endif


