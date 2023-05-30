import React, { useState } from 'react';
import { Form, FormGroup, Label, Input, Button, Container, Row, Col } from 'reactstrap';
import { useNavigate } from 'react-router-dom';
import { login } from '../services/dataService';
import { ToastContainer, toast } from 'react-toastify';


function LoginPage({ history }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();


    const handleSubmit = async (e) => {
        e.preventDefault();
        
        try {
            const response = await login(username, password);
            if (response) { // Assuming login function resolves if successful
                toast.success("Logged in successfully!"); // Show success toast
                navigate('/workers'); // Redirect to '/worker' page
            } else {
                throw new Error('Login failed');
            }
        } catch (error) {
            toast.error("Login failed. Please check your username and password."); // Show error toast
            console.error('An error occurred while trying to log in:', error);
        }
    };

    return (
        <>    <ToastContainer />
        
        <Container className="mt-5">
            <Row className="justify-content-md-center">
                <Col xs={12} md={6}>
                    <Form onSubmit={handleSubmit}>
                        <h2 className="text-center mb-4">Login</h2>
                        <FormGroup>
                            <Label for="username">Username</Label>
                            <Input 
                                type="text"
                                name="username"
                                id="username"
                                placeholder="Enter username" 
                                value={username} 
                                onChange={(e) => setUsername(e.target.value)} 
                                required
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="password">Password</Label>
                            <Input 
                                type="password"
                                name="password"
                                id="password"
                                placeholder="Password" 
                                value={password} 
                                onChange={(e) => setPassword(e.target.value)} 
                                required
                            />
                        </FormGroup>
                        <Button color="primary" type="submit" className="w-100 mt-3">
                            Submit
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
        </>
    );
    
}


export default LoginPage;
