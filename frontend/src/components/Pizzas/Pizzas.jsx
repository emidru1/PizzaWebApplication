import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Card, CardContent, CardActions, Button, Grid, Typography, Container, Box, CardMedia } from '@mui/material';
import useFetch from '../useFetch';
import './Pizzas.css';
import smallPizzaImg from '../../images/small_pizza.jpg';
import mediumPizzaImg from '../../images/medium_pizza.jpg';
import largePizzaImg from '../../images/large_pizza.jpg';
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
            <Typography component="h1" variant="h2" align="center" color="text.primary" className="pizza-header">
                Pizza List
            </Typography>
            <Typography variant="h5" align="center" color="text.secondary" component="p" className="pizza-subheader">
                Choose your pizza size to proceed with your order
            </Typography>
        </Container>

        <Container maxWidth="md" component="main">
            <Grid container spacing={10} alignItems="flex-end">
                {
                    pizzas.map(pizza => (
                        <Grid item key={pizza.id} xs={12} md={4}>
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