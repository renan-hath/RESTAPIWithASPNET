import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import * as constants from "./constants.jsx";
import Login from "./pages/Login";
import Books from "./pages/Books";
import NewBook from "./pages/NewBook";

export default function AppRoutes() {
	return (
		<BrowserRouter>
			<Routes>
				<Route path={constants.CLIENT_ROUTE_LOGIN} element={<Login />} />
				<Route path={constants.CLIENT_ROUTE_BOOKS} element={<Books />} />
				<Route
					path={`${constants.CLIENT_ROUTE_NEWBOOK}/:bookId`}
					element={<NewBook />}
				/>
			</Routes>
		</BrowserRouter>
	);
}
