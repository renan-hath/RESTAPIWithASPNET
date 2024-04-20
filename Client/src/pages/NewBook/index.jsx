import React from "react";
import "./styles.css";
import * as constants from "../../constants";
import { Link } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";

export default function NewBook() {
	return (
		<div className="new-book-container">
			<div className="content">
				<section className="form">
					<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
					<h1>Add new book</h1>
					<Link className="back-link" to="/books">
						<FiArrowLeft size={30} color="#251FC5" />
					</Link>
				</section>
				<form>
					<input placeholder="Title" />
					<input placeholder="Author" />
					<input placeholder="Price" />
					<input type="date" />

					<button type="submit" className="button">
						Add
					</button>
				</form>
			</div>
		</div>
	);
}
