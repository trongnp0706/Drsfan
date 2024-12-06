// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Drsfan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrsfanBookWeb.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "State")]
            public string State { get; set; }

            [Display(Name = "Postal Code")]
            public string PostalCode { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var name = user.Name;
            var streetAddress = user.StreetAddress;
            var city = user.City;
            var state = user.State;
            var postalCode = user.PostalCode;

            Username = userName;

            Input = new InputModel
            {
                Name = name,
                StreetAddress = streetAddress,
                City = city,
                State = state,
                PostalCode = postalCode,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User) as ApplicationUser;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var currentName = user.Name;
            if (Input.Name != currentName)
            {
                user.Name = Input.Name;
                var setNameResult = await _userManager.UpdateAsync(user);
                if (!setNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set name.";
                    return RedirectToPage();
                }
            }

            var currentStreetAddress = user.StreetAddress;
            if (Input.StreetAddress != currentStreetAddress)
            {
                user.StreetAddress = Input.StreetAddress;
                var setStreetAddressResult = await _userManager.UpdateAsync(user);
                if (!setStreetAddressResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set street address.";
                    return RedirectToPage();
                }
            }

            var currentCity = user.City;
            if (Input.City != currentCity)
            {
                user.City = Input.City;
                var setCityResult = await _userManager.UpdateAsync(user);
                if (!setCityResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set city.";
                    return RedirectToPage();
                }
            }

            var currentState = user.State;
            if (Input.State != currentState)
            {
                user.State = Input.State;
                var setStateResult = await _userManager.UpdateAsync(user);
                if (!setStateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set state.";
                    return RedirectToPage();
                }
            }

            var currentPostalCode = user.PostalCode;
            if (Input.PostalCode != currentPostalCode)
            {
                user.PostalCode = Input.PostalCode;
                var setPostalCodeResult = await _userManager.UpdateAsync(user);
                if (!setPostalCodeResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set postal code.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
