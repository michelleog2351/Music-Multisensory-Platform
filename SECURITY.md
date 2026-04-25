# Security Policy

## Supported Versions

| Version | Supported |
| ------- | --------- |
| 1.0     | ✅        |
| < 1.0   | ❌        |

---

## Reporting a Vulnerability

If you discover a security vulnerability within this project, please report it responsibly.

### How to report:
- Open a private issue on GitHub OR
- Contact the developer directly via email (if applicable)

### What to include:
- Description of the vulnerability
- Steps to reproduce
- Potential impact

### Response Time:
You can expect an initial response within 3–5 working days.

---

## Security Considerations

This project implements the following security practices:
- OAuth 2.0 authentication (Fitbit & Spotify)
- Secure token storage (server-side only)
- No exposure of API credentials in frontend
- Dependency scanning via GitHub (Dependabot, CodeQL)

---

## Known Limitations
- Relies on third-party APIs (Fitbit, Spotify)
- Token security depends on correct environment configuration
- Recalibration logic not fully implemented / fallback mechanisms in place to secure the sytem for maintainability
