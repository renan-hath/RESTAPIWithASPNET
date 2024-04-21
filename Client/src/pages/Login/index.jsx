import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./styles.css";
import api from "./../../services/api.jsx";
import * as constants from "../../constants";

export default function Login() {
	const [userName, setUsername] = useState("");
	const [password, setPassword] = useState("");
	const requestBody = {
		userName,
		password,
	};

	const navigate = useNavigate();

	async function login(e) {
		e.preventDefault();

		try {
			const response = await api.post(
				constants.API_ENDPOINT_LOGIN,
				requestBody,
			);

			localStorage.setItem("userName", userName);
			localStorage.setItem("accessToken", response.data.accessToken);
			localStorage.setItem("refreshToken", response.data.refreshToken);

			navigate(constants.CLIENT_ROUTE_BOOKS);
		} catch (error) {
			alert("Login failed. Please try again.");
		}
	}

	return (
		<div className="login-container">
			<section className="form">
				<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
				<form onSubmit={login}>
					<h1>Access your account</h1>

					<input
						placeholder="Username"
						value={userName}
						onChange={(e) => setUsername(e.target.value)}
					/>

					<input
						type="password"
						placeholder="Password"
						value={password}
						onChange={(e) => setPassword(e.target.value)}
					/>

					<button type="submit" className="button">
						Login
					</button>
				</form>
			</section>
		</div>
	);
}
