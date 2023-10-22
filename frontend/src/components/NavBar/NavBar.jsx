import React from 'react';
import { AppBar, Toolbar, Typography } from '@mui/material';
import Link from '@mui/material/Link';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useNavigate } from 'react-router-dom';

const defaultTheme = createTheme();

export default function NavBar() {
    const navigate = useNavigate();

    return(
        <AppBar
            position="static"
            color="default"
            elevation={0}
            sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
        >
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between' }}>
                <Typography variant="h5" color="inherit" noWrap>
                    Pizza Ordering App
                </Typography>
                <nav sx={{ ml: 'auto' }}>
                    <Link
                        variant="button"
                        color="text.primary"
                        href="/"
                        sx={{ my: 1, mx: 1.5 }}
                    >
                        Home
                    </Link>
                    <Link
                        variant="button"
                        color="text.primary"
                        href="/orders"
                        sx={{ my: 1, mx: 1.5 }}
                    >
                        My orders
                    </Link>
                </nav>
            </Toolbar>
        </AppBar>
    );
}
