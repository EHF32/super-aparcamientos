﻿using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace SuperAparcamiento.Functions.Extensions;

public static class XLWorkbookExtensions
{
    /// <summary>
    /// Convierte un XLWorkbook en un FileStreamResult para ser devuelto en una respuesta HTTP
    /// </summary>
    public static FileStreamResult DeliverToHttpResponse(this XLWorkbook workbook, string fileName, string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
    {
        var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new FileStreamResult(memoryStream, contentType)
        {
            FileDownloadName = fileName
        };
    }
}