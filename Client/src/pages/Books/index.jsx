import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from "react-icons/fi";
import "./styles.css";
import api from "./../../services/api.jsx";
import * as constants from "../../constants";

export default function Books() {
	const [books, setBooks] = useState([]);
	const [page, setPage] = useState(1);
	const booksPerPage = 6;

	const userName = localStorage.getItem("userName");
	const accessToken = localStorage.getItem("accessToken");
	const requestHeaders = {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	};
	const navigate = useNavigate();

	useEffect(() => {
		readBooks();
	}, [accessToken]);

	async function readBooks() {
		const response = await api.get(
			`${constants.API_ENDPOINT_BOOK}/
             ${constants.API_ENDPOINT_BOOK_SORTDIRECTION.ASC}/
             ${booksPerPage}/
             ${page}`,
			requestHeaders,
		);
		setBooks([...books, ...response.data.list]);
		setPage(page + 1);
	}

	async function deleteBook(id) {
		try {
			await api.delete(`${constants.API_ENDPOINT_BOOK}/${id}`, requestHeaders);

			setBooks(books.filter((book) => book.id !== id));
		} catch (error) {
			alert("Error while trying to delete this book. Please try again.");
		}
	}

	async function editBook(id) {
		try {
			navigate(`${constants.CLIENT_ROUTE_NEWBOOK}/${id}`);
		} catch (error) {
			alert("Error while starting to edit this book. Please try again.");
		}
	}

	async function logout() {
		try {
			await api.get(constants.API_ENDPOINT_LOGOUT, requestHeaders);

			localStorage.clear();
			navigate(constants.CLIENT_ROUTE_LOGIN);
		} catch (error) {
			alert("Logout failed. Please try again.");
		}
	}

	return (
		<div className="book-container">
			<header>
				<img src={constants.LOGO_PATH} alt="ASPNETReactApp Logo" />
				<span>
					Welcome, <strong>{userName.toLowerCase()}</strong>!
				</span>
				<Link className="button" to={`${constants.CLIENT_ROUTE_NEWBOOK}/0`}>
					Add new book
				</Link>
				<button type="button" onClick={logout}>
					<FiPower size={18} color="#251FC5" />
				</button>
			</header>

			<h1>Your books</h1>
			<ul>
				{books.map((book) => (
					<li key={book.id}>
						<strong>Title:</strong>
						<p>{book.title}</p>

						<strong>Author:</strong>
						<p>{book.author}</p>

						<strong>Price:</strong>
						<p>
							{Intl.NumberFormat("pt-BR", {
								style: "currency",
								currency: "BRL",
							}).format(book.price)}
						</p>

						<strong>Release date:</strong>
						<p>
							{Intl.DateTimeFormat("pt-BR").format(new Date(book.launchDate))}
						</p>

						<button type="button" onClick={() => editBook(book.id)}>
							<FiEdit size={20} color="#251FC5" />
						</button>
						<button type="button" onClick={() => deleteBook(book.id)}>
							<FiTrash2 size={20} color="#251FC5" />
						</button>
					</li>
				))}
			</ul>
			<button type="button" className="button" onClick={readBooks}>
				Load more
			</button>
		</div>
	);
}
