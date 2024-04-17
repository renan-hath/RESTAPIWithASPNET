import React from "react";
import "./styles.css";
import * as constants from "../../constants";

export default function Login() {
	return (
		<div className="login-container">
			<section className="login-form">
				<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
				<form>
					<h1>Access your account</h1>
					<input placeholder="Username" />
					<input type="password" placeholder="Password" />

					<button type="submit" className="button">
						Login
					</button>
				</form>
			</section>
		</div>
	);
}
