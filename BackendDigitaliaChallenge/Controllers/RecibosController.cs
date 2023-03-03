using BackendDigitaliaChallenge.Context;
using BackendDigitaliaChallenge.Models;
using BackendDigitaliaChallenge.Models.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace BackendDigitaliaChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosController : ControllerBase
    {
        private AppDbContext context;
        readonly IGeneratePdf generatePdf;
        public RecibosController(AppDbContext context, IGeneratePdf generatePdf)
        {
            this.context = context;
            this.generatePdf = generatePdf; 
        }

        [HttpGet]
        [Route("GetAllRecibos/{idUsuario}")]
        public async Task<IActionResult> GetAllRecibos(int idUsuario)
        {
            try
            {
                var recibos = await context.recibo.Where(r => r.usuarioId == idUsuario).ToListAsync();


                return Ok(new
                {
                    res = true,
                    StatusCode = 200,
                    Message = "",
                    Data = recibos
                });

            } catch (Exception e)
            {
                return BadRequest(new
                {
                    res = false,
                    StatusCode = 500,
                    Message = "Ocurrio un error " + e.Message,
                    Data = ""
                });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetRecibo/{idRecibo}")]
        public async Task<IActionResult> GetReciboById(int idRecibo)
        {
            try
            {
                var recibo = await context.recibo.Where(r => r.id == idRecibo).FirstOrDefaultAsync();

                if(recibo == null)
                {
                    return StatusCode(404, new
                        {
                            Res = false,
                            StatusCode = 404,
                            Message = "No se encontro el recibo",
                            Data = " " 
                        });
                }

                return Ok(new
                {
                    res = true,
                    StatusCode = 200,
                    Message = "",
                    Data = recibo
                });

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    res = false,
                    StatusCode = 500,
                    Message = "Ocurrio un error " + e.Message,
                    Data = ""
                });
            }
        }

        [HttpPost]
        //[Route("CreateRecibo")]
        public async Task<IActionResult> CreateRecibo([FromBody] CreateReciboRequest request)
        {
            try
            {
                var existUsu = await context.usuario.FirstOrDefaultAsync(u => u.id == request.usuarioId);

                if(existUsu == null)
                {
                    return BadRequest(new
                    {
                        res = false,
                        StatusCode = 500,
                        Message = "El usuario no existe ",
                        Data = ""
                    });
                }

                await context.recibo.AddAsync(new Recibo
                {
                    usuarioId = request.usuarioId,
                    moneda = request.moneda,
                    monto = request.monto,
                    titulo = request.titulo,
                     descripcion = request.descripcion,
                     direccion = request.direccion,
                    nombres = request.nombres,
                     apellidos = request.apellidos,
                    tipoDoc =request.tipoDoc,
                    numDoc = request.numDoc
                });
                await context.SaveChangesAsync();

                return Ok(new
                {
                    res = true,
                    StatusCode = 200,
                    Message = "El recibo se creo correctamente",
                    Data = ""
                });

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    res = false,
                    StatusCode = 500,
                    Message = "Ocurrio un error " + e.Message,
                    Data = ""
                });
            }
        }

        [HttpGet]
        [Route("generarPdf/{idRecibo}")]
        public async Task<IActionResult> generarPdf(int idRecibo)
        {
            try
            {
                Recibo recibo = await context.recibo.FirstAsync(r => r.id == idRecibo);
                return await generatePdf.GetPdf("Views/Pdfs/Recibo.cshtml", recibo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
