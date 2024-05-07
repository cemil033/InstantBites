using InstantBites.Application.Features.Commands.Clients.ClearClientNotifications;
using InstantBites.Application.Features.Commands.Clients.ConfirmEmailClient;
using InstantBites.Application.Features.Commands.Clients.DeactivateClient;
using InstantBites.Application.Features.Commands.Clients.LoginClient;
using InstantBites.Application.Features.Commands.Clients.RegistrationClient;
using InstantBites.Application.Features.Commands.Clients.UpdateClient;
using InstantBites.Application.Features.Queries.Clients.GetClientNotifications;
using InstantBites.Application.Features.Queries.Clients.GetInfoClient;
using InstantBites.Application.Features.Queries.Clients.LogOutClient;
using InstantBites.MVC.Areas.Admin.Controllers;
using InstantBites.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Diagnostics;
using System.Net;
using Azure.Core;
using InstantBites.Application.Features.Commands.Clients.ResetPasswordClient;
using InstantBites.Infrastructure.Services;
using InstantBites.Domain.Services;
using InstantBites.Application.Features.Queries.Clients.GetClientOrders;
using InstantBites.Application.Services;

namespace InstantBites.MVC.Controllers
{
    [Authorize(Roles = "Client")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;



        public AccountController(IMediator mediator, ILogger<AccountController> logger, IEmailService emailService)
        {
            _mediator = mediator;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<IActionResult> GetInfo()
        {
            try
            {
                var res = await _mediator.Send(new GetInfoClientQueryRequest());
                if (res.Success)
                {
                    var client = new UpdateClientCommandRequest()
                    {
                        Active = res.Client.Active,
                        Balance = res.Client.Balance,
                        Email = res.Client.Email,
                        Latitude = res.Client.ClientLocation.Latitude,
                        Longitude = res.Client.ClientLocation.Longitude,
                        Name = res.Client.Name,
                        PhoneNumber = res.Client.PhoneNumber,
                        SignedUrl = res.Client.SignedUrl,
                        UserName = res.Client.UserName,
                        Surname = res.Client.Surname
                    };
                    return View(client);
                }

                _logger.LogError($"{DateTime.Now} :: Was Not Get Information User");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginClientCommandRequest request, string? returnUrl)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var res = await _mediator.Send(request);

                    if (res.Success)
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else if (!res.Success && res.Key == "Email")
                    {
                        return RedirectToAction("Result", "Home", new { success = false, message = "You have not confirmed your email yet!" });
                    }
                    else
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        _logger.LogError($"{DateTime.Now}::{res.Key} / {res.Value}");
                        ModelState.AddModelError("Email", "Email or Password incorrect");
                        return View(request);
                    }
                }
                _logger.LogError($"{DateTime.Now} :: Incorrect username or password");
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> SignUp()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([Bind("Name,Surname,Balance,Image,Longitude,Latitude,SignedUrl,Email,Password,PhoneNumber,UserName")] RegistrationClientCommandRequest request)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var res = await _mediator.Send(request);
                    if (res.Success)
                    {
                        await _emailService.Send(request.Email, true, Url);
                        return RedirectToAction("Result", "Home", new { success = true, message = "Check your email." }); ;
                    }
                    else
                    {
                        _logger.LogError($"{DateTime.Now} :: {res.Title} / Was not Registered");
                        Error();
                    }
                }
                _logger.LogError($"{DateTime.Now} :: Model state is not valid");
                ModelState.AddModelError("Registration", "Check your Information");
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string email)
        {
            try
            {
                var res = await _mediator.Send(new ConfirmEmailClientCommandRequest() { Email = email });
                if (res.Success)
                    return RedirectToAction("Result", "Home", new { success = true, message = "Email Confirmation Success!Sign In your profile." });
                else
                {
                    _logger.LogError($"Email Confirmation Unsuccess");
                    return RedirectToAction("Result", "Home", new { success = false, message = "Email Confirmation Unsuccess!" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                await _emailService.Send(email, false, Url);
                return RedirectToAction("Result", "Home", new { success = true, message = "Check your email." }); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                ViewBag.Email = email;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([Bind("Email,Password")] ResetPasswordClientCommandRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _mediator.Send(request);
                    if (res.Success)
                        return RedirectToAction("Result", "Home", new { success = true, message = "Your password is reset!Sign in your profile" }); ;
                    return RedirectToAction("Result", "Home", new { success = false, message = "Your password is not reset!" });
                }
                ViewBag.Email = request.Email;
                _logger.LogError($"{DateTime.Now} :: Password is not valid");
                ModelState.AddModelError("Reset Password", "Password is not valid");
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            try
            {
                var res = await _mediator.Send(new LogOutClientQueryRequest());
                if (res.Success) return RedirectToAction("Index", "Home");
                _logger.LogError($"{DateTime.Now} :: Was Not LogOut");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        [HttpPost]
        public async Task<IActionResult> GetInfo([Bind("Name,Surname,Balance,Image,SignedUrl,Longitude,Latitude,CurrentPassword,Email,Password,PhoneNumber,UserName")] UpdateClientCommandRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _mediator.Send(request);
                    if (res.Success)
                    {
                        if (res.IsChangeEmail == true)
                        {
                            await _emailService.Send(request.Email, true, Url);
                            return RedirectToAction("LogOut", "Account");
                        }
                        return RedirectToAction("Result", "Home", new { success = true, message = "Your Profile is updated" });
                    }


                    _logger.LogError($"{DateTime.Now} :: User is not updated");
                    ModelState.AddModelError(res.Results.First().Key, res.Results.First().Value);
                    return View(request);
                }
                else
                {
                    _logger.LogError($"{DateTime.Now} :: User Model State is not valid");
                    return View(request);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
        public async Task<IActionResult> Deactivate()
        {
            try
            {
                var res = await _mediator.Send(new DeactivateClientCommandRequest());
                if (res.Success) RedirectToAction("Result", "Home", new { success = false, message = "Your profile is deactivated" });
                _logger.LogError($"{DateTime.Now} :: User is not Deactivated");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        public async Task<IActionResult> GetSubscribedOrders()
        {
            try
            {
                var res = await _mediator.Send(new ClientGetOrdersQueryRequest());
                return Ok(res.Orders);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Notifications()
        {
            try
            {
                var res = await _mediator.Send(new GetClientNotificationsQueryRequest());
                return Ok(res.Notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        public async Task<IActionResult> ClearNotfications()
        {
            try
            {
                var res = await _mediator.Send(new ClearClientNotificationsCommandRequest());

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} :: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
