export const CLIENT_ROUTE_LOGIN = "/";
export const CLIENT_ROUTE_BOOKS = "/books";
export const CLIENT_ROUTE_NEWBOOK = "/book/new";

export const API_VERSION = "1";
export const API_PATH = "https://localhost:44300";
export const API_ENDPOINT_LOGIN = `/api/auth/v${API_VERSION}/signin`;
export const API_ENDPOINT_BOOKS = `/api/book/v${API_VERSION}/asc/6/1`;
export const API_ENDPOINT_BOOK = `/api/book/v${API_VERSION}`;

export const ASSETS_PATH = "./assets/";
export const LOGO_PATH = require(`${ASSETS_PATH}logo.png`);
export const PADLOCK_PATH = require(`${ASSETS_PATH}padlock.png`);
