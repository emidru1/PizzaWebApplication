import React from 'react';
import { useNavigate } from 'react-router-dom';
import useFetch from '../App/useFetch.js';
import { Card, CardContent, CardActions, Button, Grid, Typography, Container, Box, CardMedia } from '@mui/material';
import smallPizzaImg from '../../images/small_pizza.jpg';
import mediumPizzaImg from '../../images/medium_pizza.jpg';
import largePizzaImg from '../../images/large_pizza.jpg';
import './Pizzas.css';

export default function Pizzas() {
    const { data: pizzas, error } = useFetch('http://localhost:5290/api/Pizza/sizes');
    const navigate = useNavigate();

    if (!pizzas) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;

    const getPizzaImage = (pizzaName) => {
        switch (pizzaName.toLowerCase()) {
            case 'small':
                return smallPizzaImg;
            case 'medium':
                return mediumPizzaImg;
            case 'large':
                return largePizzaImg;
            default:
                return '';
        }
    }

    return (
    <div>
        <Container disableGutters maxWidth="sm" component="main" className="pizza-header-container">
            <Typography component="h1" variant="h2" align="center" className="pizza-header">
                Pizza List
            </Typography>
            <Typography variant="h5" align="center" component="p" className="pizza-subheader">
                Choose your pizza size to proceed with your order
            </Typography>
        </Container>

        <Container maxWidth="md" component="main">
            <Grid container spacing={10} alignItems="flex-end">
                {
                    pizzas.map(pizza => (
                        <Grid item key={pizza.id} xs={12} sm={6} md={4}>
                            <Box className="pizza-card-box">
                                <Card className="pizza-card">
                                    <CardMedia
                                        component="img"
                                        height="140"
                                        image={getPizzaImage(pizza.name)}
                                        alt={pizza.name}
                                        className="pizza-image"
                                    />
                                    <CardContent>
                                        <Typography variant="h5" align="center" className="pizza-name">
                                            {pizza.name}
                                        </Typography>
                                        <Typography component="h4" variant="h4" color="text.primary" align="center" className="pizza-price">
                                            ${pizza.price}
                                        </Typography>
                                    </CardContent>
                                    <CardActions>
                                        <Button fullWidth variant="contained" color="primary" className="pizza-order-button" onClick={() => navigate('/order', { state: { pizza: pizza } })}>
                                            Order
                                        </Button>
                                    </CardActions>
                                </Card>
                            </Box>
                        </Grid>
                    ))
                }
            </Grid>
        </Container>
    </div>
);

}