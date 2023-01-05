import React, { useState, useEffect } from "react";
import { Modal, Button, Row, Col, Form } from 'react-bootstrap';

export default function EditApproverModal(props) {
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

    function handleSubmit(event) {
        event.preventDefault();

        const postToUpdate = {
            BTA_APPROVER_ID: props.btaapprover.BTA_APPROVER_ID,
            USER_ID: props.btaapprover.USER_ID,
            APPROVER: props.btaapprover.APPROVER,
            SEQ_NO: props.btaapprover.SEQ_NO,
            CREATED_BY: null,
            CREATED_DATE: null,
            LAST_USER: 'KINCHA',
            LAST_UPDATE: null
        };

        fetch('https://localhost:7195/api/BTAApprovers', {
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

        props.onHide(postToUpdate);
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
                <Modal.Title id="contained-modal-title-vcenter">Edit Approver</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Col>
                        <Form id="editApprover" onSubmit={handleSubmit}>
                            <Form.Group controlId="UserID">
                                <Form.Label>UserID</Form.Label>
                                <Form.Control as="select" name="USER_ID" onChange={props.handleChange} defaultValue={props.btaapprover.APPROVER}>
                                    {users.map(user =>
                                        <option value={user.USER_ID}>{user.USER_NAME}</option>)}
                                </Form.Control>
                            </Form.Group>
                            <Form.Group controlId="Sequence">
                                <Form.Label>Sequence</Form.Label>
                                <Form.Control as="select" name="SEQ_NO" onChange={props.handleChange} defaultValue={props.btaapprover.SEQ_NO}>
                                    <option value={1}>1</option>
                                    <option value={2}>2</option>
                                    <option value={3}>3</option>
                                </Form.Control>

                            </Form.Group>
                            <Form.Group controlId="Approver">
                                <Form.Label>Approver</Form.Label>
                                <Form.Control as="select" name="APPROVER" onChange={props.handleChange} defaultValue={props.btaapprover.APPROVER}>
                                    {approvers.map(approver =>
                                        <option value={approver.USER_ID}>{approver.USER_NAME}</option>)}
                                </Form.Control>
                            </Form.Group>
                        </Form>
                    </Col>
                </Row>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => { props.onHide(null) }}>
                    Close
                </Button>
                <Button form="editApprover" variant="primary" type="Submit">Save Changes</Button>
            </Modal.Footer>
        </Modal>
    );
}