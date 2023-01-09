import React, { useState, useEffect } from "react";
import { Accordion, Button, Container, Row, Col, Form } from 'react-bootstrap';

export default function EditRequest(props) {

    const [formData, setFormData] = useState({
        REQUEST_ID: '',
        REQUESTED_BY: '',
        REQUEST_DATE: '',
        REQUEST_STATUS: '',
        DEPARTMENT: '',
        PASSPORT_NAME: '',
        JOB_TITLE: '',
        PASSPORT_TYPE: '',
        EXTN_NO: '',
        PURPOSE: '',
        AIR_TICKET: '',
        HOTEL_RESERVATION: '',
        OFFSHORE_CAR_SERVICE: '',
        OFFSHORE_CAR_REASON: '',
        COST_CENTER1: '',
        COST_CENTER1_PERCENTAGE: '',
        COST_CENTER2: '',
        COST_CENTER2_PERCENTAGE: '',
        COST_CENTER3: '',
        COST_CENTER3_PERCENTAGE: '',
        COST_CENTER4: '',
        COST_CENTER4_PERCENTAGE: '',
        COST_CENTER5: '',
        COST_CENTER5_PERCENTAGE: '',
        TRIP_ADVANCE_DAY: '',
        TRIP_ADVANCE_WEEK: '',
        TRIP_ADVANCE_SPECIAL: '',
        TRIP_ADVANCE_REASON: '',
        FIRST_APPROVER: '',
        FIRST_APPROVAL_DATE: '',
        SECOND_APPROVER: '',
        SECOND_APPROVAL_DATE: '',
        THIRD_APPROVER: '',
        THIRD_APPROVAL_DATE: '',
        CURRENT_APPROVER: '',
        CREATED_BY: '',
        CREATED_DATE: '',
        LAST_USER: '',
        LAST_UPDATE: '',
        PASSPORT_OTHER: '',
        TICKET_REMARK: '',
        HOTEL_REMARK: '',
        RETURN_TO_TRAVELER_REMARK: '',
        COST_CENTER_6: '',
        COST_CENTER_6_PERCENTAGE: '',
        DEPARTMENT_OTHER: '',
        TRAVEL_TYPE: '',
        LOCATION_CODE: '',
        HR_APPROVER: '',
        HOME_VISIT: '',
        HR_APPROVAL_DATE: ''
    });
    const [btaHotels, setBTAHotels] = useState([]);
    const [btaTickets, setBTATickets] = useState([]);
    const [departments, setDepartments] = useState([]);
    const [travelTypes, setTravelTypes] = useState([]);
    const [travelClasses, setTravelClasses] = useState([]);
    const [currencies, setCurrencies] = useState([]);

    function handleSubmit(event) {
        event.preventDefault();

        const postToUpdate = {
            REQUEST_ID: formData.REQUEST_ID,
            REQUESTED_BY: formData.REQUESTED_BY,
            REQUEST_DATE: formData.REQUEST_DATE,
            REQUEST_STATUS: formData.REQUEST_STATUS,
            DEPARTMENT: formData.DEPARTMENT,
            PASSPORT_NAME: formData.PASSPORT_NAME,
            JOB_TITLE: formData.JOB_TITLE,
            PASSPORT_TYPE: formData.PASSPORT_TYPE,
            EXTN_NO: formData.EXTN_NO,
            PURPOSE: formData.PURPOSE,
            AIR_TICKET: formData.AIR_TICKET,
            HOTEL_RESERVATION: formData.HOTEL_RESERVATION,
            OFFSHORE_CAR_SERVICE: formData.OFFSHORE_CAR_SERVICE === "on" ? "Y" : "N",
            OFFSHORE_CAR_REASON: formData.OFFSHORE_CAR_REASON,
            COST_CENTER1: formData.COST_CENTER1,
            COST_CENTER1_PERCENTAGE: formData.COST_CENTER1_PERCENTAGE,
            COST_CENTER2: formData.COST_CENTER2,
            COST_CENTER2_PERCENTAGE: formData.COST_CENTER2_PERCENTAGE,
            COST_CENTER3: formData.COST_CENTER3,
            COST_CENTER3_PERCENTAGE: formData.COST_CENTER3_PERCENTAGE,
            COST_CENTER4: formData.COST_CENTER4,
            COST_CENTER4_PERCENTAGE: formData.COST_CENTER4_PERCENTAGE,
            COST_CENTER5: formData.COST_CENTER5,
            COST_CENTER5_PERCENTAGE: formData.COST_CENTER5_PERCENTAGE,
            TRIP_ADVANCE_DAY: formData.TRIP_ADVANCE_DAY,
            TRIP_ADVANCE_WEEK: formData.TRIP_ADVANCE_WEEK,
            TRIP_ADVANCE_SPECIAL: formData.TRIP_ADVANCE_SPECIAL,
            TRIP_ADVANCE_REASON: formData.TRIP_ADVANCE_REASON,
            FIRST_APPROVER: formData.FIRST_APPROVER,
            FIRST_APPROVAL_DATE: formData.FIRST_APPROVAL_DATE,
            SECOND_APPROVER: formData.SECOND_APPROVER,
            SECOND_APPROVAL_DATE: formData.SECOND_APPROVAL_DATE,
            THIRD_APPROVER: formData.THIRD_APPROVER,
            THIRD_APPROVAL_DATE: formData.THIRD_APPROVAL_DATE,
            CURRENT_APPROVER: formData.CURRENT_APPROVER,
            CREATED_BY: formData.CREATED_BY,
            CREATED_DATE: formData.CREATED_DATE,
            LAST_USER: 'KINCHA',
            LAST_UPDATE: null,
            PASSPORT_OTHER: formData.PASSPORT_OTHER,
            TICKET_REMARK: formData.TICKET_REMARK,
            HOTEL_REMARK: formData.HOTEL_REMARK,
            RETURN_TO_TRAVELER_REMARK: formData.RETURN_TO_TRAVELER_REMARK,
            COST_CENTER_6: formData.COST_CENTER_6,
            COST_CENTER_6_PERCENTAGE: formData.COST_CENTER_6_PERCENTAGE,
            DEPARTMENT_OTHER: formData.DEPARTMENT_OTHER,
            TRAVEL_TYPE: formData.TRAVEL_TYPE,
            LOCATION_CODE: formData.LOCATION_CODE,
            HR_APPROVER: formData.HR_APPROVER,
            HOME_VISIT: formData.HOME_VISIT === "on" ? "Y" : "N",
            HR_APPROVAL_DATE: formData.HR_APPROVAL_DATE
        };

        fetch('https://localhost:7195/api/BTARequests', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postToUpdate)
        })
            .then(res => res.json())
            .then((result) => {
                alert(result);
            }, (error) => {
                alert(error);
            })

        props.onHide();
    }

    function handleChange(event) {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value,
        });
    };

    useEffect(() => {
        fetch('https://localhost:7195/api/BTARequests/GetDepartments')
            .then(response => response.json())
            .then(data => setDepartments(data));

        fetch('https://localhost:7195/api/BTARequests/GetTravelTypes')
            .then(response => response.json())
            .then(data => setTravelTypes(data));

        fetch('https://localhost:7195/api/BTARequests/GetTravelClasses')
            .then(response => response.json())
            .then(data => setTravelClasses(data));

        fetch('https://localhost:7195/api/BTARequests/GetCurrencies')
            .then(response => response.json())
            .then(data => setCurrencies(data));

        fetch('https://localhost:7195/api/BTATickets/' + props.btareq.REQUEST_ID)
            .then(response => response.json())
            .then(data => setBTATickets(data));

        fetch('https://localhost:7195/api/BTAHotels/' + props.btareq.REQUEST_ID)
            .then(response => response.json())
            .then(data => setBTAHotels(data));
    });


    return (
        <Container>
            <h1>BTA Request</h1>
            <Form id="editRequest" onSubmit={handleSubmit}>
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
                                        <Col sm="6">
                                            <Form.Control type="text" name="REQUESTED_BY" placeholder="Your Name" value={props.btareq.REQUESTED_BY} onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="DEPARTMENT">
                                        <Form.Label column sm="4">
                                            Department
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control as="select" name="DEPARTMENT" onChange={handleChange}>
                                                {departments.map(department =>
                                                    <option key={department.RECORD_CODE} value={department.RECORD_CODE}>{department.RECORD_DESC}</option>)}
                                            </Form.Control>
                                        </Col>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="PASSPORT_NAME">
                                        <Form.Label column sm="4">
                                            Name on Passport
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" name="PASSPORT_NAME" value={props.btareq.PASSPORT_NAME} placeholder="Name on Passport" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="JOB_TITLE">
                                        <Form.Label column sm="4">
                                            Job Title
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" name="JOB_TITLE" value={props.btareq.JOB_TITLE} placeholder="Job Title" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="PASSPORT_TYPE">
                                        <Form.Label column sm="4">
                                            Type of Passport
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" name="PASSPORT_TYPE" value={props.btareq.PASSPORT_TYPE} placeholder="Type of Passport" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="EXTN_NO">
                                        <Form.Label column sm="4">
                                            Phone Number
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" name="EXTN_NO" value={props.btareq.EXTN_NO} placeholder="Extension Number" onChange={handleChange} />
                                        </Col>
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
                                            <Form.Check type="checkbox" name="HOME_VISIT" value={props.btareq.HOME_VISIT} onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="LOCATION_CODE">
                                        <Form.Label column sm="4">
                                            Office Location
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" name="LOCATION_CODE" value={props.btareq.LOCATION_CODE} placeholder="Office Location" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Form.Group className="mb-3" controlId="PURPOSE">
                                    <Form.Label column sm="4">Purpose of Trip</Form.Label>
                                    <Form.Control as="textarea" rows={3} name="PURPOSE" value={props.btareq.PURPOSE} placeholder="Trip Purpose" onChange={handleChange} />
                                </Form.Group>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="TRAVEL_TYPE">
                                        <Form.Label column sm="4">
                                            Travel Type
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control as="select" name="TRAVEL_TYPE" onChange={handleChange}>
                                                {travelTypes.map(travelType =>
                                                    <option key={travelType.RECORD_CODE} value={travelType.RECORD_CODE}>{travelType.RECORD_DESC}</option>)}
                                            </Form.Control>
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col></Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="OFFSHORE_CAR_SERVICE">
                                        <Col sm="2">
                                            <Form.Check type="checkbox" name="OFFSHORE_CAR_SERVICE" onChange={handleChange} />
                                        </Col>
                                        <Form.Label column sm="6">Offshore Car Pick-up Service</Form.Label>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="OFFSHORE_CAR_REASON">
                                        <Form.Label column sm="4">Reason</Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.OFFSHORE_CAR_REASON} name="OFFSHORE_CAR_REASON" placeholder="Reason Offshore Car Pick-up Service" onChange={handleChange} />
                                        </Col>
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
                                        <Col sm="12">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER1} name="COST_CENTER1" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER1_PERCENTAGE">
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER1_PERCENTAGE} name="COST_CENTER1_PERCENTAGE" onChange={handleChange} />
                                        </Col>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER2">
                                        <Col sm="12">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER2} name="COST_CENTER2" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER2_PERCENTAGE">
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER2_PERCENTAGE} name="COST_CENTER2_PERCENTAGE" onChange={handleChange} />
                                        </Col>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER3">
                                        <Col sm="12">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER3} name="COST_CENTER3" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER3_PERCENTAGE">
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER3_PERCENTAGE} name="COST_CENTER3_PERCENTAGE" onChange={handleChange} />
                                        </Col>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER4">
                                        <Col sm="12">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER4} name="COST_CENTER4" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER4_PERCENTAGE">
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER4_PERCENTAGE} name="COST_CENTER4_PERCENTAGE" onChange={handleChange} />
                                        </Col>
                                        <Form.Label column sm="4">
                                            %
                                        </Form.Label>
                                    </Form.Group>
                                </Col>
                            </Row>
                            <Row>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER5">
                                        <Col sm="12">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER5} name="COST_CENTER5" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="COST_CENTER5_PERCENTAGE">
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.COST_CENTER5_PERCENTAGE} name="COST_CENTER5_PERCENTAGE" onChange={handleChange} />
                                        </Col>
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
                                    <Col sm="6">
                                        <Form.Control type="text" value={props.btareq.TRIP_ADVANCE_DAY} name="TRIP_ADVANCE_DAY" onChange={handleChange} />
                                    </Col>
                                    <Form.Label column sm="4">
                                        Day at $(USD)
                                    </Form.Label>
                                </Form.Group>
                            </Row>
                            <Row>
                                <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_WEEK">
                                    <Col sm="6">
                                        <Form.Control type="text" value={props.btareq.TRIP_ADVANCE_WEEK}  name="TRIP_ADVANCE_WEEK" onChange={handleChange} />
                                    </Col>
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
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.TRIP_ADVANCE_SPECIAL} name="TRIP_ADVANCE_SPECIAL" onChange={handleChange} />
                                        </Col>
                                    </Form.Group>
                                </Col>
                                <Col>
                                    <Form.Group as={Row} className="mb-3" controlId="TRIP_ADVANCE_REASON">
                                        <Form.Label column sm="4">
                                            Reason
                                        </Form.Label>
                                        <Col sm="6">
                                            <Form.Control type="text" value={props.btareq.TRIP_ADVANCE_REASON} name="TRIP_ADVANCE_REASON" onChange={handleChange} />
                                        </Col>
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
            <Button form="editRequest" variant="primary" type="Submit" >
                Submit
            </Button>
        </Container>
    );
}

