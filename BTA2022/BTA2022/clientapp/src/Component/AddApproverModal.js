import React, { useState, useEffect } from "react";
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';

export default function AddApproverModal(props) {
    const [formData, setFormData] = useState({
        USER_ID: '',
        APPROVER: '',
        SEQ_NO: '',
    });

    const [approvers, setApprovers] = useState([]);
    const [users, setUsers] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7195/api/BTAApprovers/GetAllApproverNames')
            .then(response => response.json())
            .then(data => setApprovers(data));

        fetch('https://localhost:7195/api/BTAApprovers/GetAllUserNames')
            .then(response => response.json())
            .then(data => setUsers(data));
    }, []);

    function handleChange(event) {
        setFormData({
            ...formData,
            [event.target.name]: event.target.value,
        });
    };

    function handleSubmit(event) {
        event.preventDefault();

        const postToCreate = {
            BTA_APPROVER_ID: 0,
            USER_ID: event.target.USER_ID.value,
            APPROVER: event.target.APPROVER.value,
            SEQ_NO: event.target.SEQ_NO.value,
            CREATED_BY: 'KINCHA',
            CREATED_DATE: null,
            LAST_USER: 'KINCHA',
            LAST_UPDATE: null
        };

        fetch('https://localhost:7195/api/BTAApprovers', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postToCreate)
        })
        .then(res => res.json())
        .then((result) => {
            alert(result);
        }, (error) => {
            alert(error);
        })

        props.onHide();
    }

    return (
        <Modal
            show={props.show}
            onHide={props.onHide}
            backdrop="static"
            keyboard={false}
            aria-labelledby="contained-modal-title-vcenter"
            size="lg"
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">Add Approver</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Col>
                        <Form id="addApprover" onSubmit={handleSubmit}>
                            <Form.Group controlId="userid">
                                <Form.Label>UserID</Form.Label>
                                <Form.Control as="select" name="USER_ID" onChange={handleChange}>
                                    <option value={""}> </option>
                                    {users.map(user =>
                                        <option value={user.USER_ID}>{user.USER_NAME}</option>)}
                                </Form.Control>
                            </Form.Group>
                            <Form.Group controlId="sequence">
                                <Form.Label>Sequence</Form.Label>
                                <Form.Control as="select" name="SEQ_NO" onChange={handleChange}>
                                    <option value={1}>1</option>
                                    <option value={2}>2</option>
                                    <option value={3}>3</option>
                                </Form.Control>
                                
                            </Form.Group>
                            <Form.Group controlId="approver">
                                <Form.Label>Approver</Form.Label>
                                <Form.Control as="select" name="APPROVER" onChange={handleChange}> 
                                    <option value={""}> </option>
                                    {approvers.map(approver =>
                                        <option value={approver.USER_ID}>{approver.USER_NAME}</option>)}
                                </Form.Control>
                            </Form.Group>
                        </Form>
                    </Col>
                </Row>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.onHide}>
                    Close
                </Button>
                <Button form="addApprover" variant="primary" type="Submit">Save Changes</Button>
            </Modal.Footer>
        </Modal>
    );
}