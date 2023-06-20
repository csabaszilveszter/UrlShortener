#!/bin/bash
dacpac="false"
sqlfiles="false"
dacpath=$2
sqlpath=$3

echo "SELECT * FROM SYS.DATABASES" | dd of=testsqlconnection.sql
for i in {1..60};
do
    sqlcmd -S db -U sa -d master -i testsqlconnection.sql > /dev/null
    if [ $? -eq 0 ]
    then
        echo "SQL server ready"
        break
    else
        echo "Not ready yet..."
        sleep 1
    fi
done
rm testsqlconnection.sql

for f in $dacpath/*
do
    if [ $f == $dacpath/*".dacpac" ]
    then
        dacpac="true"
        echo "Found dacpac $f"
    fi
done

for f in $sqlpath/*
do
    if [ $f == $sqlpath/*".sql" ]
    then
        sqlfiles="true"
        echo "Found SQL file $f"
    fi
done

if [ $sqlfiles == "true" ]
then
    for f in $sqlpath/*
    do
        if [ $f == $sqlpath/*".sql" ]
        then
            echo "Executing $f"
            sqlcmd -S db -U sa -d master -i $f
        fi
    done
fi

if [ $dacpac == "true" ] 
then
    for f in $dacpath/*
    do
        if [ $f == $dacpath/*".dacpac" ]
        then
            dbname=$(basename $f ".dacpac")
            echo "Deploying dacpac $f"
            sqlpackage /Action:Publish /SourceFile:$f /TargetServerName:db /TargetDatabaseName:$dbname /TargetUser:sa /TargetPassword:$SQLCMDPASSWORD
        fi
    done
fi

dotnet dev-certs https --trust

dotnet tool install -g dotnet-aspnet-codegenerator --version 6.0.14
dotnet tool install -g dotnet-ef --version 7.0.7

dotnet restore ./Csaba.UrlShortener.sln

dotnet ef database update -p ./src/Csaba.UrlShortener.Api/Csaba.UrlShortener.Api.csproj
