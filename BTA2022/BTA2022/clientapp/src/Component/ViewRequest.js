import React, { useState, useEffect } from "react";
import { Accordion, Button, Container, Row, Col, Form } from 'react-bootstrap';

export default function ViewRequest(props) {

    const [btaHotels, setBTAHotels] = useState([]);
    const [btaTickets, setBTATickets] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7195/api/BTATickets/' + props.btareq.REQUEST_ID)
            .then(response => response.json())
            .then(data => setBTATickets(data));

        fetch('https://localhost:7195/api/BTAHotels/' + props.btareq.REQUEST_ID)
            .then(response => response.json())
            .then(data => setBTAHotels(data));
    })

    return (
        <Container>
            <h1>BTA Request</h1>
            <Form id="editRequest">
                <Accordion defaultActiveKey="0">
                    <Accordion.Item eventKey="0">
                        <Accordion.Header>General Information</Accordion.Header>
                        <Accordion.Body>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="REQUESTED_BY">
                                        <Form.Label column sm="4">
                                            Name of Travaller
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.REQUESTED_BY}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="DEPARTMENT">
                                        <Form.Label column sm="4">
                                            Department
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.DEPARTMENT}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="PASSPORT_NAME">
                                        <Form.Label column sm="4">
                                            Name on Passport
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.PASSPORT_NAME}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="JOB_TITLE">
                                        <Form.Label column sm="4">
                                            Job Title
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.JOB_TITLE}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="PASSPORT_TYPE">
                                        <Form.Label column sm="4">
                                            Type of Passport
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.PASSPORT_TYPE}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="EXTN_NO">
                                        <Form.Label column sm="4">
                                            Phone Number
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.EXTN_NO}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="HOME_VISIT">
                                        <Form.Label column sm="4">
                                            Home Visit
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Check type="checkbox" name="HOME_VISIT" value={props.btareq.HOME_VISIT} disabled />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="LOCATION_CODE">
                                        <Form.Label column sm="4">
                                            Office Location
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.LOCATION_CODE}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Form.Group className="mb-3" controlId="PURPOSE">
                                    <Form.Label column sm="4">Purpose of Trip</Form.Label>
                                    <Form.Control as="textarea" rows={3} name="PURPOSE" value={props.btareq.PURPOSE} placeholder="Trip Purpose" disabled />
                                </Form.Group>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="TRAVEL_TYPE">
                                        <Form.Label column sm="4">
                                            Travel Type
                                        </Form.Label>
                                        <Form.Label column sm="4">
                                            {props.btareq.TRAVEL_TYPE}
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col></Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="OFFSHORE_CAR_SERVICE">
                                        <Col sm="2">
                                            <Form.Check type="checkbox" name="OFFSHORE_CAR_SERVICE" disabled />
                                        </Col>
                                        <Form.Label column sm="6">Offshore Car Pick-up Service</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="OFFSHORE_CAR_REASON">
                                        <Form.Label column sm="4">Reason</Form.Label>
                                        <Form.Label column sm="4">{props.btareq.OFFSHORE_CAR_REASON}</Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="1">
                        <Accordion.Header>Ticket</Accordion.Header>
                        <Accordion.Body>
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
                                    </tr>
                                </thead>
                                <tbody>
                                    {btaTickets.map((btaTicket) => (
                                        <tr key={btaTicket.BTA_TICKET_ID}>
                                            <td>{btaTicket.TICKET_DATE}</td>
                                            <td>{btaTicket.TICKET_FROM}</td>
                                            <td>{btaTicket.TICKET_TO}</td>
                                            <td>{btaTicket.TICKET_CLASS}</td>
                                            <td>{btaTicket.FLIGHT_DETAILS}</td>
                                            <td>{btaTicket.CURRENCY}</td>
                                            <td>{btaTicket.EXCHANGE_RATE}</td>
                                            <td>{btaTicket.FLIGHT_FARE}</td>
                                            <td>{btaTicket.TICKET_SURCHARGE}</td>
                                            <td></td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="2">
                        <Accordion.Header>Hotel</Accordion.Header>
                        <Accordion.Body>
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
                                    </tr>
                                </thead>
                                <tbody>
                                    {btaHotels.map((btaHotel) => (
                                        <tr key={btaHotel.BTA_HOTEL_BOOKINGS_ID}>
                                            <td>{btaHotel.CHECK_IN_DATE}</td>
                                            <td>{btaHotel.CHECK_OUT_DATE}</td>
                                            <td>{btaHotel.HOTEL_NAME}</td>
                                            <td>{btaHotel.NUM_NIGHTS}</td>
                                            <td>{btaHotel.CURRENCY}</td>
                                            <td>{btaHotel.EXCHANGE_RATE}</td>
                                            <td>{btaHotel.HOTLE_FARE_PER_NIGHT}</td>
                                            <td>{btaHotel.HOTEL_SURCHARE}</td>
                                            <td>{btaHotel.SERVICE_CHARGE}</td>
                                            <td></td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="3">
                        <Accordion.Header>Cost Center</Accordion.Header>
                        <Accordion.Body>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER1">
                                        <Form.Label column sm="12">{props.btareq.COST_CENTER1}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER1_PERCENTAGE">
                                        <Form.Label column sm="6">{props.btareq.COST_CENTER1_PERCENTAGE}</Form.Label>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER2">
                                        <Form.Label column sm="12">{props.btareq.COST_CENTER2}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER2_PERCENTAGE">
                                        <Form.Label column sm="6">{props.btareq.COST_CENTER2_PERCENTAGE}</Form.Label>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER3">
                                        <Form.Label column sm="12">{props.btareq.COST_CENTER3}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER3_PERCENTAGE">
                                        <Form.Label column sm="6">{props.btareq.COST_CENTER3_PERCENTAGE}</Form.Label>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER4">
                                        <Form.Label column sm="12">{props.btareq.COST_CENTER4}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER4_PERCENTAGE">
                                        <Form.Label column sm="6">{props.btareq.COST_CENTER4_PERCENTAGE}</Form.Label>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER5">
                                        <Form.Label column sm="12">{props.btareq.COST_CENTER5}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER5_PERCENTAGE">
                                        <Form.Label column sm="6">{props.btareq.COST_CENTER5_PERCENTAGE}</Form.Label>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="4">
                        <Accordion.Header>Trip Advance Request</Accordion.Header>
                        <Accordion.Body>
                            <Row>
                                <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_DAY">
                                    <Form.Label column sm="6">{props.btareq.TRIP_ADVANCE_DAY}</Form.Label>
                                    <Form.Label column sm="4">
                                        Day at $(USD)
                                    </Form.Label>
                                </Form.Group>
                            </Row>
                            <Row>
                                <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_WEEK">
                                    <Form.Label column sm="6">{props.btareq.TRIP_ADVANCE_WEEK}</Form.Label>
                                    <Form.Label column sm="4">
                                        Week at $(USD)
                                    </Form.Label>
                                </Form.Group>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_SPECIAL">
                                        <Form.Label column sm="4">
                                            Special $(USD)
                                        </Form.Label>
                                        <Form.Label column sm="6">{props.btareq.TRIP_ADVANCE_SPECIAL}</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_REASON">
                                        <Form.Label column sm="4">
                                            Reason
                                        </Form.Label>
                                        <Form.Label column sm="6">{props.btareq.TRIP_ADVANCE_REASON}</Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                        </Accordion.Body>
                    </Accordion.Item>
                </Accordion>
            </Form>
            <Button variant="secondary" onClick={props.onHide}>
                Close
            </Button>
        </Container>
    );
}

