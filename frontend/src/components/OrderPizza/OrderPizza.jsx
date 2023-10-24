import { React, useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { Container, Typography, Box, FormControlLabel, Checkbox, Button, Card, CardContent } from '@mui/material';
import useFetch from '../App/useFetch';
import './OrderPizza.css';
export default function OrderPizza() {
    const location = useLocation();
    const { data: toppings, error } = useFetch('http://localhost:5290/api/Pizza/toppings');
    const [order, setOrder] = useState({});
    const [feedbackMessage, setFeedbackMessage] = useState('');
    const size = location.state?.pizza.name || '';
    const pizza = location.state?.pizza || {};
    const [selectedToppings, setSelectedToppings] = useState([]);
    const [orderCost, setOrderCost] = useState(0);  
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        const order = {
            sizeName: size,
            toppingNames: selectedToppings
        };
        try {
            const response = await fetch('http://localhost:5290/api/Order', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(order)
            });
    
            if (!response.ok) {
                throw new Error('Failed to submit pizza order data');
            }
            const result = await response.json();
            setOrder(order);
            setFeedbackMessage('Order submitted successfully');
            setSelectedToppings([]);
        } catch (error) {
            console.error('Error submitting data:', error);
        }
        
    }
    useEffect(() => {
        const fetchOrderTotal = async () => {
            const tempOrder = {
                sizeName: size,
                toppingNames: selectedToppings
            };
            
            try {
                const response = await fetch('http://localhost:5290/api/Order/CalculateTotal', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(tempOrder)
                });
        
                if (!response.ok) {
                    throw new Error('Failed to fetch order total');
                }
                const result = await response.json();
                setOrderCost(result.orderTotal);
            } catch (error) {
                console.error('Error fetching order total:', error);
            }
        };
        fetchOrderTotal();
    }, [size, selectedToppings]);

    if(!toppings) return <div>Loading...</div>;
    if(error) return <div>Error: {error}</div>;


    return (
        <Container maxWidth="md" component="main">
            <Box className="order-header-container">
                <Typography component="h1" variant="h2" align="center" color="text.primary" className="order-header">
                    Order a Pizza
                </Typography>
            </Box>
    
            <Card className="order-card">
                <CardContent>
                    <form onSubmit={handleSubmit}>
                        <Typography variant="h5" align="center" className="pizza-size">
                            Pizza size: {size}
                        </Typography>
                        {feedbackMessage && (
                            <Typography variant="h4" align="center" color="success.main" className="feedback-message">
                                {feedbackMessage}
                            </Typography>
                        )}
                        <Box className="toppings-box">
                            <Typography variant="h6" className="toppings-header">
                                Choose your toppings:
                            </Typography>
                            {toppings.map(topping => (
                                <FormControlLabel
                                    key={topping.id}
                                    control={
                                        <Checkbox 
                                            value={topping.name} 
                                            checked={selectedToppings.includes(topping.name)}
                                            onChange={(e) => {
                                                if(e.target.checked) {
                                                    setSelectedToppings([...selectedToppings, topping.name]);
                                                } else {
                                                    setSelectedToppings(selectedToppings.filter(t => t !== topping.name));
                                                }
                                            }}
                                        />
                                    }
                                    label={topping.name}
                                />
                            ))}
                        </Box>
    
                        <Typography variant="h6" className="order-total">
                            Order total: ${orderCost}
                        </Typography>
    
                        <Box className="submit-button-box">
                            <Button variant="contained" color="primary" fullWidth type="submit">
                                Submit
                            </Button>
                        </Box>
                    </form>
                </CardContent>
            </Card>
        </Container>
    );
}