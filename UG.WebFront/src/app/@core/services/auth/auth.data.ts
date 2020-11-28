export class AUTH_DATA {
    public  static GetUserStorageKey(): string {
        return 'CasheePublicAudio_currentUser';
    }

    public  static CLAIMS_KEYS: any = {
        // Common JWT token keys
        AUDIENCE: 'aud',
        EXPIRATION: 'exp',
        ROLES: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
        ISSUER: 'iss',
        KEY: 'jti',
        USERNAME: 'sub',
        EMAIL: 'email',

        // Cashee's custom keys
        // COMPANY_ID: 'CompanyId',
        // CASHBOX_ID: 'CashBoxId',
        // GALLERY_NAME: 'GalleryName'
    };
}
