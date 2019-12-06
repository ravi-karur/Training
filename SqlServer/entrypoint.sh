#!/bin/bash
set -e

sleep 20s

#run the setup script to create the DB and the schema in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Jko3va-D9821jhsvGD -d master -i setup.sql

exec "$@"