import React, { useState } from 'react';
import { Card, Form, Button, Alert } from 'react-bootstrap';

export default function Login(props) {

    const [userID, setUserID] = useState("");
    const [password, setPassword] = useState("");

    function handleSubmit(event) {
        event.preventDefault();
        props.onLogin(userID, password);
    }

    function handleUserID(event) {
        setUserID(event.target.value);
    }

    function handlePassword(event) {
        setPassword(event.target.value);
    }

    return (
        <Card>
            <Card.Body>
                <h2 className="text-center mb-4">Log In</h2>
                <Form onSubmit={handleSubmit}>
                    <Form.Group id="USER_ID">
                        <Form.Label>User ID</Form.Label>
                        <Form.Control type="input" onChange={handleUserID} required></Form.Control>
                    </Form.Group>
                    <Form.Group id="PASSWORD">
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="password" onChange={handlePassword} required></Form.Control>
                    </Form.Group>
                    <Button className="w-100" type="submit">Log In</Button>
                </Form>
            </Card.Body>
        </Card>
    )
}