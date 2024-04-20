import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./pages/Login";
import Books from "./pages/Books";
import NewBook from "./pages/NewBook";

export default function AppRoutes() {
	return (
		<BrowserRouter>
			<Routes>
				<Route path="/" exact element={<Login />} />
				<Route path="/books" exact element={<Books />} />
				<Route path="/book/new" exact element={<NewBook />} />
			</Routes>
		</BrowserRouter>
	);
}
