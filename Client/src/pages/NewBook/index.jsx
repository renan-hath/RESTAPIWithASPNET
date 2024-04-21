import React, { useState, useEffect } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";
import "./styles.css";
import api from "./../../services/api.jsx";
import * as constants from "../../constants";

export default function NewBook() {
	const [id, setId] = useState(null);
	const [author, setAuthor] = useState("");
	const [title, setTitle] = useState("");
	const [launchDate, setLaunchDate] = useState("");
	const [price, setPrice] = useState("");
	const { bookId } = useParams();
	const accessToken = localStorage.getItem("accessToken");
	const requestHeaders = {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	};
	const navigate = useNavigate();

	useEffect(() => {
		if (bookId === "0") return;
		readBook();
	}, [bookId]);

	async function createBook(e) {
		e.preventDefault();

		const requestBody = {
			title,
			author,
			price,
			launchDate,
		};

		try {
			await api.post(constants.API_ENDPOINT_BOOK, requestBody, requestHeaders);
		} catch (error) {
			alert("Error while trying to create a new book. Please try again.");
		}

		navigate(constants.CLIENT_ROUTE_BOOKS);
	}

	async function readBook() {
		try {
			const response = await api.get(
				`${constants.API_ENDPOINT_BOOK}/${bookId}`,
				requestHeaders,
			);

			setId(response.data.id);
			setTitle(response.data.title);
			setAuthor(response.data.author);
			setPrice(response.data.price);
			setLaunchDate(response.data.launchDate.split("T", 10)[0]);
		} catch (error) {
			alert("Error while trying to load this book. Please try again.");
			navigate(constants.CLIENT_ROUTE_BOOKS);
		}
	}

	async function updateBook(e) {
		e.preventDefault();

		const requestBody = {
			id,
			title,
			author,
			price,
			launchDate,
		};

		try {
			await api.put(constants.API_ENDPOINT_BOOK, requestBody, requestHeaders);
		} catch (error) {
			alert("Error while trying to edit this book. Please try again.");
		}

		navigate(constants.CLIENT_ROUTE_BOOKS);
	}

	return (
		<div className="new-book-container">
			<div className="content">
				<section className="form">
					<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
					<h1>{bookId === "0" ? "Add new book" : "Update book"}</h1>
					<Link className="back-link" to={constants.CLIENT_ROUTE_BOOKS}>
						<FiArrowLeft size={30} color="#251FC5" />
					</Link>
				</section>

				<form onSubmit={bookId === "0" ? createBook : updateBook}>
					<input
						placeholder="Title"
						value={title}
						onChange={(e) => setTitle(e.target.value)}
					/>
					<input
						placeholder="Author"
						value={author}
						onChange={(e) => setAuthor(e.target.value)}
					/>
					<input
						placeholder="Price"
						value={price}
						onChange={(e) => setPrice(e.target.value)}
					/>
					<input
						type="date"
						value={launchDate}
						onChange={(e) => setLaunchDate(e.target.value)}
					/>

					<button type="submit" className="button">
						{bookId === "0" ? "Add" : "Update"}
					</button>
				</form>
			</div>
		</div>
	);
}
