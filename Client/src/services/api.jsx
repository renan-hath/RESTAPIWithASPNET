import axios from "axios";
import * as constants from "./../constants.jsx";

const api = axios.create({
	baseURL: constants.API_PATH,
});

export default api;
