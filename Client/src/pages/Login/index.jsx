import React from "react";
import api from "./../../services/api.jsx";
import "./styles.css";
import { useNavigate } from "react-router-dom";
import * as constants from "../../constants";

export default function Login() {
	const [userName, setUsername] = React.useState("");
	const [password, setPassword] = React.useState("");
	const navigate = useNavigate();

	async function login(e) {
		e.preventDefault();

		const data = {
			userName,
			password,
		};

		try {
			const response = await api.post(constants.API_ENDPOINT_LOGIN, data);

			localStorage.setItem("userName", userName);
			localStorage.setItem("accessToken", response.data.accessToken);
			localStorage.setItem("refreshToken", response.data.refreshToken);

			navigate("/books");
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
