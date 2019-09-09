@echo off

pushd "Eshop.Catalog"
start func start %~s1

pushd "../Eshop.Promotions"
start func start %~s1

pushd "../Eshop.Reviews"
start func start %~s1

pushd "../Eshop.ApiGateway"
start dotnet run

exit