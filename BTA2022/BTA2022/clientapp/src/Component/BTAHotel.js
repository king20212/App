import React from "react";
import { Form } from 'react-bootstrap';

export default function BTAHotel(props) {

    return (
        <div>
            <table className="table table-bordered border-dark">
                <thead>
                    <tr>
                        <th scope="col">From (Check-in date)</th>
                        <th scope="col">To (Check-out date</th>
                        <th scope="col">Name of Hotel</th>
                        <th scope="col">Num of Night</th>
                        <th scope="col">Currency</th>
                        <th scope="col">Exchange Rate</th>
                        <th scope="col">Hotel Rate per Night</th>
                        <th scope="col">Surcharge & Tax(%)</th>
                        <th scope="col">Service Charge(%)</th>
                        <th scope="col">Total (USD)</th>
                        <th scope="col">Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {props.btahotels.map((btahotel, index) => (
                        <tr key={index}>
                            <td><Form.Control type="date" name="CHECK_IN_DATE" value={btahotel.CHECK_IN_DATE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="date" name="CHECK_OUT_DATE" value={btahotel.CHECK_OUT_DATE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="HOTEL_NAME" value={btahotel.HOTEL_NAME} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="HOTEL_SURCHARE" value={btahotel.HOTEL_SURCHARE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="HOTLE_FARE_PER_NIGHT" value={btahotel.HOTLE_FARE_PER_NIGHT} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="NUM_NIGHTS" value={btahotel.NUM_NIGHTS} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="CURRENCY" value={btahotel.CURRENCY} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="EXCHANGE_RATE" value={btahotel.EXCHANGE_RATE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
                            <td><Form.Control type="text" name="SERVICE_CHARGE" value={btahotel.SERVICE_CHARGE} onChange={(e) => { props.onEdit(e, index) }}></Form.Control></td>
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
                <button type="button" className="btn btn-primary" onClick={props.onAdd}>Add Hotel</button>
            </div>
        </div>
    );
}
