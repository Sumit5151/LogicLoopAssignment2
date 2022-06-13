using Assignment2.Model;
using Assignment2.ServiceInterface;
using Assignment2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace Assignment2.Controllers
{
    [Authorize]
    [Route("")]
    [ApiController]
    public class LogicLoopController : ControllerBase
    {

        public IFixerService1 _iFixerService1;
        public IFixerService2 _iFixerService2;
        public LogicLoopController(IFixerService1 iFixerService1, IFixerService2 iFixerService2)
        {
            _iFixerService1 = iFixerService1;
            _iFixerService2 = iFixerService2;

        }


        [HttpGet("1.0/{fromCurrency}/{fromCurrencyAmount}/{toCurrency}")]
        public IActionResult GetV1(string fromCurrency, decimal fromCurrencyAmount, string toCurrency)
        {
            try
            {
                //FixerServiceV1 fixerServiceV1 = new FixerServiceV1();
                var fixerResponseTask = Task.Run(() => _iFixerService1.ConvertCurrencyV1(fromCurrency, fromCurrencyAmount, toCurrency));
                fixerResponseTask.Wait();

                FixerResponseModelV1 fixerResponse = fixerResponseTask.Result;

                if (fixerResponse.success == true)
                {
                    var successMessage = Convert.ToString(fixerResponse.query.amount) + " " + fixerResponse.query.from + " in " + fixerResponse.query.to + " = " + fixerResponse.result;

                    var successResponse = new
                    {
                        Success = true,
                        Info = new
                        {
                            Message = successMessage
                        }
                    };

                    return Ok(successResponse);
                }

                else
                {
                    var errorMessage = fixerResponse.error.info;
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                return BadRequest(ex.ToString());
            }
        }



        //version 2 added date parameter for version 2  format YYYY-MM-DD
        [HttpGet("2.0/{fromCurrency}/{fromCurrencyAmount}/{toCurrency}/{date}")]
        public IActionResult GetV2(string fromCurrency, decimal fromCurrencyAmount, string toCurrency, string date)
        {
            try
            {
                //FixerServiceV2 fixerServiceV2 = new FixerServiceV2();
                var fixerResponseTask = Task.Run(() => _iFixerService2.ConvertCurrencyV2(fromCurrency, fromCurrencyAmount, toCurrency, date));
                fixerResponseTask.Wait();

                FixerResponseModelV2 fixerResponse = fixerResponseTask.Result;

                if (fixerResponse.success == true)
                {
                    var successMessage = Convert.ToString(fixerResponse.query.amount) + " " + fixerResponse.query.from + " in " + fixerResponse.query.to + " = " + fixerResponse.result;

                    var successResponse = new
                    {
                        Success = true,
                        Info = new
                        {
                            Message = successMessage,
                            Date = date
                        }
                    };

                    return Ok(successResponse);
                }

                else
                {
                    var errorMessage = fixerResponse.error.info;
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                return BadRequest(ex.ToString());
            }
        }
    }
}
