import React from "react";
import { Form } from 'react-bootstrap';

export default function BTATicket(props) {

    return (
        <div>
            <table className="table table-bordered border-dark">
                <thead>
                    <tr>
                        <th scope="col">Date</th>
                        <th scope="col">From (City)</th>
                        <th scope="col">To (City)</th>
                        <th scope="col">Class</th>
                        <th scope="col">Flight/Vessel/Train No(Depart./Arrival Time)</th>
                        <th scope="col">Currency</th>
                        <th scope="col">Exchange Rate</th>
                        <th scope="col">Estimate Flight Fare</th>
                        <th scope="col">Surcharge & Tax</th>
                        <th scope="col">Total (USD)</th>
                        <th scope="col">Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {props.btatickets.map((btaticket, index) => (
                        <tr key={index}>
                            <td><Form.Control type="date" name="TICKET_DATE" value={btaticket.TICKET_DATE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="TICKET_FROM" value={btaticket.TICKET_FROM} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="TICKET_TO" value={btaticket.TICKET_TO} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="TICKET_CLASS" value={btaticket.TICKET_CLASS} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="FLIGHT_DETAILS" value={btaticket.FLIGHT_DETAILS} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="CURRENCY" value={btaticket.CURRENCY} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="EXCHANGE_RATE" value={btaticket.EXCHANGE_RATE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="FLIGHT_FARE" value={btaticket.FLIGHT_FARE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="TICKET_SURCHARGE" value={btaticket.TICKET_SURCHARGE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td></td>
                            <td>
                                <div className="btn-group" role="group">
                                    <button type="button" className="btn btn-danger" onClick={props.onDelete}>Delete</button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div className="btn-group" role="group">
                <button type="button" className="btn btn-primary" onClick={props.onAdd}>Add Ticket</button>
            </div>
        </div>
    );
}
