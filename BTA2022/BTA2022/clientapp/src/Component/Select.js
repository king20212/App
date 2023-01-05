import React from "react";

export default function Select(props) {
    return (
        <div>
        <label>{ props.title }</label>
        <select className="form-select" aria-label="Default select example">
            <option selected>Open this select menu</option>
            <option value="1">One</option>
            <option value="2">Two</option>
            <option value="3">Three</option>
            </select>
        </div>
    );
}
