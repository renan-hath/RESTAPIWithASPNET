import React from "react";
import "./styles.css";
import * as constants from "../../constants";
import { Link } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";

export default function Books() {
	return (
		<div className="book-container">
			<header>
				<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
				<span>
					Welcome, <strong>reader</strong>!
				</span>
				<Link className="button" to="book/new">
					Add new book
				</Link>
				<button type="button">
					<FiPower size={18} color="#251FC5" />
				</button>
			</header>

			<h1>Registered books</h1>
			<ul>
				<li>
					<strong>Title:</strong>
					<p>The Lord of the Rings</p>

					<strong>Author:</strong>
					<p>J. R. R. Tolkien</p>

					<strong>Price:</strong>
					<p>$19.99</p>

					<strong>Release date:</strong>
					<p>29/07/1954</p>

					<button type="button">
						<FiEdit size={20} color="#251FC5" />
					</button>
					<button type="button">
						<FiTrash2 size={20} color="#251FC5" />
					</button>
				</li>
			</ul>
		</div>
	);
}
