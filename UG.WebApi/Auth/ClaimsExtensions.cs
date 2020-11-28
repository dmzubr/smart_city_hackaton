using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace UG.WebApi.Auth
{
    public static class ClaimsExtensions
    {
        public static readonly string CompanyIdClaimName = "CompanyId";
        public static readonly string CashBoxIdClaimName = "CashBoxId";
        public static readonly string CompanyGalleryName = "GalleryName";
        public static readonly string ClientId = "ClientId";
        public static readonly string ClientPhoneNumber = "ClientPhoneNumber";
        public static readonly string APIClientPermissionsList = "APIClientPermissionsList";
        public static readonly string AppliedLicenseTypeIds = "AppliedLicenseTypeIds";

        // Keys for public audio functionality
        public static readonly string AudioPublicAccountIdClaimName = "AudioPublicAccountId";
        public static readonly string UserIdClaimName = "UserId";
    }
}
