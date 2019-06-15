alter table ВрачSet alter column ВнешнийХэш nvarchar(max) null;
alter table ВодительSet alter column ВнешнийХэш nvarchar(max) null;
alter table МедосмотрАвтоматическийSet add ВнешнийХэш nvarchar(max) null;
alter table МедосмотрСВрачомSet add ВнешнийХэш nvarchar(max) null;