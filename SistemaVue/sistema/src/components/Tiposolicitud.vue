<template>
    <v-layout align-start>
        <v-flex>
            <v-toolbar flat color="white">
                <v-toolbar-title>Tipo de solicitud</v-toolbar-title>
                <v-divider
                class="mx-2"
                inset
                vertical
                ></v-divider>
                <v-spacer></v-spacer>
                <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                <v-spacer></v-spacer>
 
            </v-toolbar>

            <v-data-table
                :headers="headers"
                :items="tiposolicitudes"
                :search="search"
                class="elevation-1"
            >
               
                <template slot="items" slot-scope="props">
                    <td>{{ props.item.nombre }}</td>
                    <td>{{ props.item.descripcion }}</td>                  
                </template>

                <template v-slot:[`item.condicion`]="{ item }">
                    <div v-if="item.condicion==1">
                        <span class="blue--text">Activo</span>
                    </div>
                    <div v-if="item.condicion==0">
                        <span class="red--text">Inactivo</span>
                    </div>
                </template>
                    

                  
                <template slot="no-data">
                    <v-btn color="primary" @click="listar">No hay datos</v-btn>
                </template>
            </v-data-table>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data(){
            return {
                tiposolicitudes: [],
                
                dialog: false,
                headers: [    
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Descripcion', value: 'descripcion', sortable: false },
                    { text: 'Estado', value: 'condicion' }
   
                ],
                search: '',
   
            }
        },
        computed: {
            
        },

        watch: {
           
        },

        created () {
            
            this.listar();
        },
        methods:{

            listar(){
                let me=this;
                axios.get('api/tiposolicitudes/Listar').then(function(response)
                {
                    //console.log(response);
                    me.tiposolicitudes=response.data;

                }).catch(function(error){
                    console.log(error);
                });
            },
 

        }        
    }
</script>
