create PROC [dbo].[ListPage]
    @p_tableName          VARCHAR(MAX),			--表名
    @p_columns            VARCHAR(1000)='*',	--字段名称
   
    @sort				  VARCHAR(100),			--排序
    @page				  INT OUTPUT,			--当前页
    @rows                 INT OUTPUT,			--每页显示记录条数
    @p_totalRecords		  INT OUTPUT,			--总记录数
    @p_totalPages         INT OUTPUT			--总页数
AS
	DECLARE   @v_sql NVARCHAR(MAX)			--sql语句
	DECLARE   @v_startRecord INT			--开始显示的记录条数
	DECLARE   @v_endRecord   INT			--结束显示的记录条数
	--记录中总记录条数
	SET @v_sql = 'SELECT @A=COUNT(*) FROM ' + @p_tableName + ' WHERE 1=1';

	EXEC  sp_executesql   @v_sql,N'@A INT OUTPUT',@p_totalRecords   output   
   --验证页面记录大小
   IF (@rows < 0)
       SET @rows = 0

   --根据页大小计算总页数
   SELECT @p_totalPages=CEILING(CAST(@p_totalRecords AS FLOAT)/@rows)

   --验证页号
   IF @page < 1 
       SET @page= 1
   IF @page > @p_totalPages 
       SET @page = @p_totalPages

   --实现分页查询
   SET @v_startRecord = (@page - 1) * @rows + 1;
   SET @v_endRecord = @page * @rows;
   SET @v_sql = 'SELECT '+@p_columns+' FROM (SELECT A.*, ROW_NUMBER() over('
   IF @sort IS NOT NULL or @sort <> '' 
       SET @v_sql = @v_sql + ' ORDER BY ' + @sort ;
   SET @v_sql=@v_sql+') r FROM  (SELECT * FROM ' + @p_tableName;

   
   SET @v_sql= @v_sql + ') A ) B WHERE  r <= ' + CAST(@v_endRecord AS VARCHAR) + ' AND r >= '+ CAST(@v_startRecord AS VARCHAR);
   EXEC(@v_sql);
  PRINT @v_sql
  SELECT	@page as N'currPage',
		@p_totalRecords as N'totalCount',
		@p_totalPages as N'totalPages'